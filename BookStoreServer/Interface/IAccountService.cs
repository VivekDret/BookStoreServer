using BookStoreServer.Models;
using BookStoreServer.Models.DTOs;

namespace BookStoreServer.Interface
{
    public interface IAccountService
    {
        Task<User> RegisterUserAsync(RegisterUserDTO model);
        string LoginAsync(dynamic model);
    }
}
