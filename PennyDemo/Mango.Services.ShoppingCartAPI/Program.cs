using AutoMapper;
using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.RabbitMQSender;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Mango.Services.ShoppingCartAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            //mapper使用 連接Dto和Models
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            builder.Services.AddSingleton(mapper); //加入地圖
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //自動映射器服務

            builder.Services.AddScoped<ICartRepository, CartRepository>(); //controller建造 加上[api]後 add
            builder.Services.AddScoped<ICouponRepository, CouponRepository>(); 

            builder.Services.AddSingleton<IMessageBus,AzureServiceBusMessageBus >(); //依賴
            //rabbitMQ
            builder.Services.AddSingleton<IRabbitMQCartMessageSender, RabbitMQCartMessageSender>(); //再去CartAPIController++

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddHttpClient<ICouponRepository, CouponRepository>(u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));

            //授權
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7176/"; //身分服務器的 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            //添加SD裡面的授權
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "mango");
                });
            });



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //添加法規文案 ?
            //新增完後 swagger右邊會出現授權標示
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Services.ShoppingCartAPI", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] and your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                //安全性需求
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }

                });
            });





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //add
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}