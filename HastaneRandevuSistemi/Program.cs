using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Utility;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using HastaneRandevuSistemi.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region Localizer
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create(nameof(SharedResource), assemblyName.Name);
    });


builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var supportCultures = new List<CultureInfo>
        {

        new CultureInfo("en-US"),

        new CultureInfo("tr-TR"),};

    options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion







































// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UygulamaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UygulamaDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>();



builder.Services.AddIdentity<IdentityUser, IdentityRole>/*işin içine roller girince kod bu şekilde değişti*/(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>()/*sağdaki kısım, register'a tıklayınca hata almamak için yazılan parçalardan biri.*/.AddDefaultTokenProviders();

builder.Services.AddRazorPages();//login adminle alakalı parçalardan biri

//_doktorBransRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IDoktorBransRepository, DoktorBransRepository>();

//_doktorRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IDoktorRepository, DoktorRepository>();

//_randevupRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IRandevuRepository, RandevuRepository>();

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

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

//app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();//login adminle alakalı parçalardan biri

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

