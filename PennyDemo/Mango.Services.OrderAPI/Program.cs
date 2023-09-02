using AutoMapper;
using Mango.MessageBus;
using Mango.Services.OrderAPI.DbContexts;
using Mango.Services.OrderAPI.Extension;
using Mango.Services.OrderAPI.Messaging;
using Mango.Services.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mango.MessageBus;
using Mango.Services.OrderAPI.RabbitMQSender;

namespace Mango.Services.OrderAPI
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
            //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            //builder.Services.AddSingleton(mapper); //加入地圖
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //自動映射器服務
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            // special add
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            //add for rabbitMQ 配置服務並自動啟動
            builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
            builder.Services.AddHostedService<RabbitMQPaymentConsumer>();

            builder.Services.AddSingleton(new OrderRepository(optionBuilder.Options));
            builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
            builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();
            builder.Services.AddSingleton<IRabbitMQOrderMessageSender, RabbitMQOrderMessageSender>();

            // Add services to the container.
            builder.Services.AddControllers();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Services.CouponAPI", Version = "v1" });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseAzureServiceBusConsumer();

            app.Run();
        }
    }
}