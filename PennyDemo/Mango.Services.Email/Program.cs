using Mango.Services.Email.DbContexts;
using Mango.Services.Email.Extension;
using Mango.Services.Email.Messaging;
using Mango.Services.Email.Repository;
using Mango.Services.PaymentAPI.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Email
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IEmailRepository, EmailRepository>(); //addscopedÂiDB
            builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
            // special add
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            builder.Services.AddSingleton(new EmailRepository(optionBuilder.Options));
            builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
           

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAzureServiceBusConsumer();
            app.MapControllers();

            app.Run();
        }
    }
}