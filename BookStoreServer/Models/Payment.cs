namespace BookStoreServer.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? OrderId { get; set; }
        public virtual OrderTbl? Order { get; set; }
        public int? RefundId { get; set; }
        public virtual Refund? Refund { get; set; }
        public string? PaymentMode { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
