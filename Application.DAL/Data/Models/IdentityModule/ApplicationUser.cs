using Microsoft.AspNetCore.Identity;

namespace Application.DAL.Data.Models.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
