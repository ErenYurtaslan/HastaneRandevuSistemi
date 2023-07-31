using HastaneRandevuSistemi.Utility;
namespace HastaneRandevuSistemi.Models
{
    public class DoktorBransRepository : Repository<DoktorBrans>, IDoktorBransRepository//önce concrete sınıflar, sonra abstract sınıflar yazılır.

    //clean code için her şeyi değil de sadece sonradan implement edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public DoktorBransRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(DoktorBrans doktorBransInterface)
        {
            _uygulamaDbContext.Update(doktorBransInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
