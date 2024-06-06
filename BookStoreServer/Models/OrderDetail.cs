namespace BookStoreServer.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public virtual OrderTbl? Order { get; set; }
        public int? BookID { get; set; }
        public virtual Book? Book { get; set; }
        public int OrderQuantity { get; set; }
        public int SubTotalOrder { get; set; }
    }
}
