using Duende.IdentityServer.Hosting;
using Duende.IdentityServer.Services;
using Mango.Services.Identity;
using Mango.Services.Identity.DbContexrs;
using Mango.Services.Identity.Initializer;
using Mango.Services.Identity.Models;
using Mango.Services.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

//add
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //添加實體框架儲存. 忘記密碼時可用 ,使用token提供程序的地方


var builder2 = builder.Services.AddIdentityServer(option =>
{
    option.Events.RaiseErrorEvents = true;
    option.Events.RaiseInformationEvents = true;
    option.Events.RaiseFailureEvents = true;
    option.Events.RaiseSuccessEvents = true;
    option.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.IdentityResources)  //添加資源..
    .AddInMemoryApiScopes(SD.ApiScopes) //客戶端 
    .AddInMemoryClients(SD.Clients)
    .AddAspNetIdentity<ApplicationUser>();

//初始化身分服務器的內容
builder.Services.AddScoped<IDbInitilizer, DbInitializer>();
//token新增name role...
builder.Services.AddScoped<IProfileService, ProfileService>();


builder2.AddDeveloperSigningCredential();  //自動產生密鑰

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//add 路由後面,使用身分服務器的應用程序,確保身分服務器添加到管道中並保存製作
app.UseIdentityServer();
// add初始化身分服務器的成員 .netcore6寫法
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitilizer>();
    dbInitializer.Initialize();
}


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
