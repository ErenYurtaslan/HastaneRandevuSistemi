using HastaneRandevuSistemi.Utility;
namespace HastaneRandevuSistemi.Models
{
    public class RandevuRepository : Repository<Randevu>, IRandevuRepository //önce concrete sınıflar, sonra abstract sınıflar yazılır.

    //clean code için her şeyi değil de sadece sonradan extend edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public RandevuRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(Randevu randevuInterface)
        {
            _uygulamaDbContext.Update(randevuInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
