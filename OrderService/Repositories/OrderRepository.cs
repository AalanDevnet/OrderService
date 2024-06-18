using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
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

                using (var connection = _context.CreateConnection())
                {
                    var orderId = await connection.ExecuteScalarAsync<int>(query, order);
                    result.Success = true;
                    result.OrderId = orderId;
                    result.WaybillNumber = order.WaybillNumber;
                    result.Status = "Sukses";
                }
            }
            catch (SqlException ex)
            {
                result.Success = false;
                result.Status = "Error";
                result.Reason = HandleSqlException(ex.Number); 
            }
            catch (Exception)
            {
                result.Success = false;
                result.Status = "Error";
                result.Reason = "Unexpected error occurred.";
               
            }

            return result;
        }

        public async Task<bool> CancelOrderByWaybillNumberAsync(string waybillNumber)
        {
            var query = @"UPDATE Orders SET Status = 'Canceled' WHERE WaybillNumber = @WaybillNumber";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { WaybillNumber = waybillNumber });
                return rowsAffected > 0;
            }
        }
        public async Task<Order> GetOrderByWaybillNumberAsync(string waybillNumber)
        {
            var query = @"SELECT * FROM Orders WHERE WaybillNumber = @WaybillNumber";

            using (var connection = _context.CreateConnection())
            {
                var order = await connection.QueryFirstOrDefaultAsync<Order>(query, new { WaybillNumber = waybillNumber });
                return order;
            }
        }

        private string HandleSqlException(int errorNumber)
        {
            switch (errorNumber)
            {
                case 2627: 
                    return "Orderid tidak boleh sama";
                default:
                    return "Terjadi kesalahan dalam penyimpanan data";
            }
        }
    }
}
