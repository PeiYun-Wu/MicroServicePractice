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
              .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //�K�[����ج[�x�s. �ѰO�K�X�ɥi�� ,�ϥ�token���ѵ{�Ǫ��a��


var builder2 = builder.Services.AddIdentityServer(option =>
{
    option.Events.RaiseErrorEvents = true;
    option.Events.RaiseInformationEvents = true;
    option.Events.RaiseFailureEvents = true;
    option.Events.RaiseSuccessEvents = true;
    option.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.IdentityResources)  //�K�[�귽..
    .AddInMemoryApiScopes(SD.ApiScopes) //�Ȥ�� 
    .AddInMemoryClients(SD.Clients)
    .AddAspNetIdentity<ApplicationUser>();

//��l�ƨ����A�Ⱦ������e
builder.Services.AddScoped<IDbInitilizer, DbInitializer>();
//token�s�Wname role...
builder.Services.AddScoped<IProfileService, ProfileService>();


builder2.AddDeveloperSigningCredential();  //�۰ʲ��ͱK�_

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

//add ���ѫ᭱,�ϥΨ����A�Ⱦ������ε{��,�T�O�����A�Ⱦ��K�[��޹D���ëO�s�s�@
app.UseIdentityServer();
// add��l�ƨ����A�Ⱦ������� .netcore6�g�k
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
