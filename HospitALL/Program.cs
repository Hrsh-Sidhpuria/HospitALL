using HospitALL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HospitALL.Utilities;
using HospitALL.Repositories.Interfaces;
using HospitALL.Repositories.Implementation;
using HospitALL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();


builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender,EmailSender>();
builder.Services.AddRazorPages();   
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
DataSeeding();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{Area=Patient}/{controller=Home}/{action=Index}/{id?}");

app.Run();
void DataSeeding()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.initializer();
    }
}