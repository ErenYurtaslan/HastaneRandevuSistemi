using HastaneRandevuSistemi.Utility;
using System.Linq.Expressions;

namespace HastaneRandevuSistemi.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository //önce concrete sınıflar, sonra abstract sınıflar yazılır.
        
        //clean code için her şeyi değil de sadece sonradan extend edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(Kitap kitapInterface)
        {
            _uygulamaDbContext.Update(kitapInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
