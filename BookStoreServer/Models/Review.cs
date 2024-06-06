namespace BookStoreServer.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string ReviewComment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int? BookID { get; set; }
        public virtual Book? Book { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
