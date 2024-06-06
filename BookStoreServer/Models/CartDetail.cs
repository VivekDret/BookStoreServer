namespace BookStoreServer.Models
{
    public class CartDetail
    {
        public int CartDetailID { get; set; }
        public int? CartID { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? BookId { get; set; }
        public virtual Book? Book { get; set; }
        public int CartQuantity { get; set; }
        public int SubTotalCart { get; set; }
    }
}
