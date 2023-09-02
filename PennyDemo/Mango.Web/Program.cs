using Mango.Web.Services.IServices;
using Mango.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace Mango.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // 配置客戶端
            builder.Services.AddHttpClient<IProductService, ProductService>();  //發送http請求類別
            builder.Services.AddHttpClient<ICartService, CartService>();  //cartservice內容設置完後++
            builder.Services.AddHttpClient<ICouponService, CouponService>();
            SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"]; //appsetting.json
            SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponApi"];

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();//cartservice內容設置完後++
            builder.Services.AddScoped<ICouponService, CouponService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //增加身分授權 連接identityAPI
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                 .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
                 .AddOpenIdConnect("oidc", options =>   //+nuget包 要和配置身分服務器的客戶端與data匹配
                 {
                     options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];  //appsetting.json
                     options.GetClaimsFromUserInfoEndpoint = true;
                     options.ClientId = "mango";
                     options.ClientSecret = "secret";  //test,實際上是一串密鑰
                     options.ResponseType = "code";
                     options.ClaimActions.MapJsonKey("role", "role", "role"); //缺少的話shoppingcart會少讀取到role
                     options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                     options.TokenValidationParameters.NameClaimType = "name";
                     options.TokenValidationParameters.RoleClaimType = "role";
                     options.Scope.Add("mango");
                     options.SaveTokens = true;

                 })
                 ;


                 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();//授權前的身分驗證

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}