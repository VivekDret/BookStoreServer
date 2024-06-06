using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserProfilePic {  get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string UserGender { get; set; }
        public string UserAddress { get; set; }
        public string UserContact { get; set; }
        [NotMapped]
        public string? Token { get; set; }

        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Payment>? Payments { get; set; }
        public virtual List<OrderTbl>? Orders { get; set; }
        public virtual List<Review>? Reviews { get; set; }
    }
}
