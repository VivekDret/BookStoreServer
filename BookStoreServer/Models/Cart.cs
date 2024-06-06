namespace BookStoreServer.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int CartTotal { get; set; }
        public virtual List<CartDetail>? CartDetails { get; set; }
    }
}
