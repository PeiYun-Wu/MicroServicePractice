using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Models
{
    public class ApplicationUser :IdentityUser   //擴展身分用戶
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
