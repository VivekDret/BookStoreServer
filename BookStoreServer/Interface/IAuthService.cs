namespace BookStoreServer.Interface
{
    public interface IAuthService
    {
        public string GenerateToken(dynamic user);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
