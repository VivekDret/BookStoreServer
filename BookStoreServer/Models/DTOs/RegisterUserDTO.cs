namespace BookStoreServer.Models.DTOs
{
    public class RegisterUserDTO
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserGender { get; set; }
        public string UserAddress { get; set; }
        public string UserContact { get; set; }
    }
}
