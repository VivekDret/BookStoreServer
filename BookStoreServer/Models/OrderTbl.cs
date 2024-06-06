namespace BookStoreServer.Models
{
    public class OrderTbl
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderTotal { get; set; }
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual User? User { get; set; }
        public virtual List<OrderDetail>? OrderDetails { get; set; }
    }
}
