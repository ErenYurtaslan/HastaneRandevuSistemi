using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//Veri Tabanında EF tablosu oluşturulması için ilgili model sınıflarımızı buraya ekliyoruz.
namespace HastaneRandevuSistemi.Utility
{
    //DbContext yerine IdentityDbContext sınıfı extend ettik ki admin paneli ve user paneli sistemini oluşturalım.
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions <UygulamaDbContext> options) : base(options)
        {

        }


        public DbSet<DoktorBrans> DoktorBranslari { get; set; }

        public DbSet<Doktor> Doktorlar { get; set; }

        public DbSet<Randevu> Randevular { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
