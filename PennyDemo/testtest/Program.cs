using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace testtest
{
    public class Program
    {
        public  static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {
                  options.Authority = "https://localhost:7176/"; //身分服務器的 
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateAudience = false
                  };
              });
            builder.Services.AddOcelot();


            var app = builder.Build();
            //添加身分驗證, 因用gateway時,向web請求服務時, 需要jwtToken傳遞給各web服務器, api才會回應

            //app.MapGet("/", () => "Hello World!");
            app.UseOcelot();
            app.Run();
        }
    }
}