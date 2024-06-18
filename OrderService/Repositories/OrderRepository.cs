using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(OrderContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CreateOrderResult> CreateOrderAsync(Order order)
        {
            var result = new CreateOrderResult();

            try
            {
                var query = @"INSERT INTO Orders 
                              (WaybillNumber, ShipperName, ShipperContact, ShipperPhone, ShipperAddr, OriginCode, ReceiverName, ReceiverPhone, ReceiverAddr, ReceiverZip, DestinationCode, ReceiverArea, Qty, Weight, GoodsDesc, ServiceType, Insurance, OrderDate, ItemName, Cod, SendStartTime, SendEndTime, ExpressType, GoodsValue, Status) 
                              VALUES 
                              (@WaybillNumber, @ShipperName, @ShipperContact, @ShipperPhone, @ShipperAddr, @OriginCode, @ReceiverName, @ReceiverPhone, @ReceiverAddr, @ReceiverZip, @DestinationCode, @ReceiverArea, @Qty, @Weight, @GoodsDesc, @ServiceType, @Insurance, @OrderDate, @ItemName, @Cod, @SendStartTime, @SendEndTime, @ExpressType, @GoodsValue, @Status); 
                              SELECT CAST(SCOPE_IDENTITY() as int)";

                _logger.LogInformation("Executing CreateOrderAsync with order: {@Order}", order);

                using (var connection = _context.CreateConnection())
                {
                    var orderId = await connection.ExecuteScalarAsync<int>(query, order);
                    result.Success = true;
                    result.OrderId = orderId;
                    result.WaybillNumber = order.WaybillNumber;
                    result.Status = "Sukses";

                    _logger.LogInformation("Order created successfully with ID: {OrderId}", orderId);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while creating order with WaybillNumber: {WaybillNumber}", order.WaybillNumber);

                result.Success = false;
                result.Status = "Error";
                result.Reason = HandleSqlException(ex.Number);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating order with WaybillNumber: {WaybillNumber}", order.WaybillNumber);

                result.Success = false;
                result.Status = "Error";
                result.Reason = "Unexpected error occurred.";
            }

            return result;
        }

        public async Task<bool> CancelOrderByWaybillNumberAsync(string waybillNumber)
        {
            var query = @"UPDATE Orders SET Status = 'Canceled' WHERE WaybillNumber = @WaybillNumber";

            _logger.LogInformation("Executing CancelOrderByWaybillNumberAsync with waybillNumber: {WaybillNumber}", waybillNumber);

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { WaybillNumber = waybillNumber });
                bool success = rowsAffected > 0;

                if (success)
                {
                    _logger.LogInformation("Order with WaybillNumber: {WaybillNumber} canceled successfully", waybillNumber);
                }
                else
                {
                    _logger.LogWarning("No order found with WaybillNumber: {WaybillNumber} to cancel", waybillNumber);
                }

                return success;
            }
        }

        public async Task<Order> GetOrderByWaybillNumberAsync(string waybillNumber)
        {
            var query = @"SELECT * FROM Orders WHERE WaybillNumber = @WaybillNumber";

            _logger.LogInformation("Executing GetOrderByWaybillNumberAsync with waybillNumber: {WaybillNumber}", waybillNumber);

            using (var connection = _context.CreateConnection())
            {
                var order = await connection.QueryFirstOrDefaultAsync<Order>(query, new { WaybillNumber = waybillNumber });

                if (order != null)
                {
                    _logger.LogInformation("Order retrieved successfully for WaybillNumber: {WaybillNumber}", waybillNumber);
                }
                else
                {
                    _logger.LogWarning("No order found with WaybillNumber: {WaybillNumber}", waybillNumber);
                }

                return order;
            }
        }

        private string HandleSqlException(int errorNumber)
        {
            _logger.LogWarning("Handling SQL exception with error number: {ErrorNumber}", errorNumber);

            return errorNumber switch
            {
                2627 => "Orderid tidak boleh sama",
                _ => "Terjadi kesalahan dalam penyimpanan data"
            };
        }
    }
}
