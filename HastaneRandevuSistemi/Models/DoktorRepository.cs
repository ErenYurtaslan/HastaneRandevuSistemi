using HastaneRandevuSistemi.Utility;
using System.Drawing;

namespace HastaneRandevuSistemi.Models
{
    public class DoktorRepository : Repository<Doktor>, IDoktorRepository //önce concrete sınıflar, sonra abstract sınıflar yazılır.

    //clean code için her şeyi değil de sadece sonradan extend edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public DoktorRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(Doktor doktorInterface)
        {
            _uygulamaDbContext.Update(doktorInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
