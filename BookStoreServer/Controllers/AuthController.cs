using BookStoreServer.Interface;
using BookStoreServer.Models;
using BookStoreServer.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAccountService _accountServices;
        private readonly IAuthService _authService;
        IRepository<User> _userRepository;


        public AuthController(ILogger<AuthController> logger, IAuthService authServices, IAccountService accountServices, IRepository<User> userRepository)
        {
            _logger = logger;
            _authService = authServices;
            _userRepository = userRepository;
            _accountServices = accountServices;            
        }


        //User Registration
        [HttpPost]
        [Route("User/Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.UserPassword != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.UserEmail;

            if (await _userRepository.GetAsync(user => user.UserEmail.ToLower() == email) != null)
            {
                ModelState.AddModelError("Error", "A user with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterUserAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "User registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the admin!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }
        }

        // Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState);
            dynamic user;
            user = await _userRepository.GetAsync(user => user.UserEmail.ToLower() == loginCredentials.Email.ToLower());
            
            if (user == null) return NotFound("User not found!");

            var match = _authService.VerifyPassword(loginCredentials.Password, user.UserPassword);

            if (!match) return BadRequest("Invalid Username or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.UserPassword = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
