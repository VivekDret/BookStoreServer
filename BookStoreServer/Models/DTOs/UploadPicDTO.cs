namespace BookStoreServer.Models.DTOs
{
    public class UploadPicDTO
    {
        public int id { get; set; }
        public IFormFile file { get; set; }
    }
}
