using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateOnly? UserRegistrationDate { get; set; }
        public DateOnly? UserLastLoginDate { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
