namespace BookStoreServer.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAddress { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorContact { get; set; }
        public string AuthorGender { get; set; }

        public virtual List<Book>? Books { get; set; }
    }
}
