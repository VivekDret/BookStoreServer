using BookStoreServer.Models.DTOs;
using BookStoreServer.Models;

namespace BookStoreServer.Mappers
{
    public class RegisterToUser
    {
        User user;

        public RegisterToUser(RegisterUserDTO registerUser)
        {
            user = new User();
            user.UserFirstName = registerUser.UserFirstName;
            user.UserLastName = registerUser.UserLastName;
            user.UserEmail = registerUser.UserEmail;
            user.UserContact = registerUser.UserContact;
            user.UserAddress = registerUser.UserAddress;
            user.UserRole = "User";
            user.UserPassword = registerUser.UserPassword;
            user.UserGender = registerUser.UserGender;
            user.UserProfilePic = $"https://api.dicebear.com/5.x/initials/svg?seed={registerUser.UserFirstName}{registerUser.UserLastName}";
        }

        public User GetUser()
        {
            return user;
        }
    }
}
