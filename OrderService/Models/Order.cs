namespace OrderService.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string WaybillNumber { get; set; }
        public string ShipperName { get; set; }
        public string ShipperContact { get; set; }
        public string ShipperPhone { get; set; }
        public string ShipperAddr { get; set; }
        public string OriginCode { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverAddr { get; set; }
        public string ReceiverZip { get; set; }
        public string DestinationCode { get; set; }
        public string ReceiverArea { get; set; }
        public int Qty { get; set; }
        public double Weight { get; set; }
        public string GoodsDesc { get; set; }
        public int ServiceType { get; set; }
        public int? Insurance { get; set; }
        public DateTime OrderDate { get; set; }
        public string ItemName { get; set; }
        public int? Cod { get; set; }
        public DateTime SendStartTime { get; set; }
        public DateTime SendEndTime { get; set; }
        public string ExpressType { get; set; }
        public int GoodsValue { get; set; }
        public string Status { get; set; }
    }
}
