using BookStoreServer.Interface;
using BookStoreServer.Mappers;
using BookStoreServer.Models;
using BookStoreServer.Models.DTOs;

namespace BookStoreServer.Services
{
    public class AccountServices : IAccountService
    {
        private readonly ILogger<AccountServices> _logger;
        private readonly IAuthService _authServices;
        private readonly IRepository<User> _userRepository;


        public AccountServices(ILogger<AccountServices> logger, IAuthService authServices, IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authServices = authServices;
 
        }
        public string LoginAsync(dynamic user)
        {
            var jwt = _authServices.GenerateToken(user);

            return jwt;
        }

        public async Task<User> RegisterUserAsync(RegisterUserDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.UserPassword);
            registrationData.UserPassword = hashedPassword;

            RegisterToUser registerToUser = new RegisterToUser(registrationData);
            User userToRegister = registerToUser.GetUser();

            var createdUser = await _userRepository.CreateAsync(userToRegister);
            createdUser.UserPassword = null;

            return createdUser;
        }
    }
}
