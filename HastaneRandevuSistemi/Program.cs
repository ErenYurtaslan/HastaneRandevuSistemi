using Microsoft.EntityFrameworkCore;
using asp_udemy_proje1.Utility;
using asp_udemy_proje1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UygulamaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>/*i�in i�ine roller girince kod bu �ekilde de�i�ti*/(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>()/*sa�daki k�s�m, register'a t�klay�nca hata almamak i�in yaz�lan par�alardan biri.*/.AddDefaultTokenProviders();

builder.Services.AddRazorPages();//login adminle alakal� par�alardan biri

//_kitapTuruRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();

//_kitapRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IKitapRepository, KitapRepository>();

//_kiralamapRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

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

app.UseAuthorization();

app.MapRazorPages();//login adminle alakal� par�alardan biri

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
