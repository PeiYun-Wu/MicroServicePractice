using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Mango.GatewaySolution
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {
                  options.Authority = "https://localhost:7176/"; //�����A�Ⱦ��� 
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateAudience = false
                  };
              });
            builder.Services.AddOcelot();


            var app = builder.Build();
            //�K�[��������, �]��gateway��,�Vweb�ШD�A�Ȯ�, �ݭnjwtToken�ǻ����Uweb�A�Ⱦ�, api�~�|�^��

            //app.MapGet("/", () => "Hello World!");
            await app.UseOcelot();
            app.Run();
        }
    }
}