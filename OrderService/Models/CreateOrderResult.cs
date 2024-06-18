namespace OrderService.Models
{
    public class CreateOrderResult
    {
        public bool Success { get; set; }
        public string Status { get; set; }
        public int OrderId { get; set; }
        public string WaybillNumber { get; set; }
        public string Reason { get; set; }
    }
}
