namespace BookStoreServer.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual List<Book>? Books { get; set; }
    }
}
