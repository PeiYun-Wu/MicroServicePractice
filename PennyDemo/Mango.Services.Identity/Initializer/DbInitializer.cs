using IdentityModel;
using Mango.Services.Identity.DbContexrs;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    //使用繼承方式(依賴注入) 直接使用DB加入用戶
    public class DbInitializer : IDbInitilizer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult(); //管理員
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult(); //自訂義用戶
            }
            else { return; }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                FirstName="Ben",
                LastName="Admin"
            };
            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,adminUser.FirstName+" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin),
            }).Result;


            //創建用戶
            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                FirstName = "Ben",
                LastName = "Cust"
            };
            _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();  //passward
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,customerUser.FirstName+" "+ customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Customer),
            }).Result;
        }
    }
}
