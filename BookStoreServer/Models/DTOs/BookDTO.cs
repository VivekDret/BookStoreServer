namespace BookStoreServer.Models.DTOs
{
    public class BookDTO
    {
        public string BookTitle { get; set; }
        public string BookSerialNumber { get; set; }
        public int BookQuantity { get; set; }
        public int BookPrice { get; set; }
        public int BookYear { get; set; }
        public string? BookImageLink { get; set; }
        public int? AuthorID { get; set; }
        public int? CategoryID { get; set; }
        public IFormFile File { get; set; }

    }
}
