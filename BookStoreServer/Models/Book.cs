using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BookStoreServer.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookSerialNumber { get; set; }
        public int BookQuantity { get; set; }
        public int BookPrice { get; set; }
        public int BookYear { get; set; }
        public string BookImageLink { get; set; }
        public int? AuthorID { get; set; }
        public virtual Author? Author { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category? Category { get; set; }
        //public int? CartDetailId { get; set; }
        public virtual List<CartDetail>? CartDetails { get; set; }
        //public int? OrderDetailId { get; set; }
        public virtual List<OrderDetail>? OrdertDetails { get; set; }
        public virtual List<Review>? Reviews { get; set; }
    }
}
