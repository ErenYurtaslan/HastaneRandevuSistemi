using HastaneRandevuSistemi.Utility;
using System.Linq.Expressions;

namespace HastaneRandevuSistemi.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository //önce concrete sınıflar, sonra abstract sınıflar yazılır.
        
        //clean code için her şeyi değil de sadece sonradan extend edilen interface'in metotları gelir.
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KiralamaRepository(UygulamaDbContext context) : base(context)
        {
            _uygulamaDbContext = context;
        }

        public void Guncelle(Kiralama kiralamaInterface)
        {
            _uygulamaDbContext.Update(kiralamaInterface);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
