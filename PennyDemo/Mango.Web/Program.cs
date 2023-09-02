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

            // �t�m�Ȥ��
            builder.Services.AddHttpClient<IProductService, ProductService>();  //�o�ehttp�ШD���O
            builder.Services.AddHttpClient<ICartService, CartService>();  //cartservice���e�]�m����++
            builder.Services.AddHttpClient<ICouponService, CouponService>();
            SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"]; //appsetting.json
            SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponApi"];

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();//cartservice���e�]�m����++
            builder.Services.AddScoped<ICouponService, CouponService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //�W�[�������v �s��identityAPI
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                 .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
                 .AddOpenIdConnect("oidc", options =>   //+nuget�] �n�M�t�m�����A�Ⱦ����Ȥ�ݻPdata�ǰt
                 {
                     options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];  //appsetting.json
                     options.GetClaimsFromUserInfoEndpoint = true;
                     options.ClientId = "mango";
                     options.ClientSecret = "secret";  //test,��ڤW�O�@��K�_
                     options.ResponseType = "code";
                     options.ClaimActions.MapJsonKey("role", "role", "role"); //�ʤ֪���shoppingcart�|��Ū����role
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
            
            app.UseAuthentication();//���v�e����������

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}