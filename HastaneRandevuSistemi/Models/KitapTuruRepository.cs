using HastaneRandevuSistemi.Utility;
using System.Linq.Expressions;

namespace HastaneRandevuSistemi.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository //önce concrete sınıflar, sonra abstract sınıflar yazılır.
        
        //clean code için her şeyi değil de sadece sonradan implement edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapTuruRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(KitapTuru kitapTuruInterface)
        {
            _uygulamaDbContext.Update(kitapTuruInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
