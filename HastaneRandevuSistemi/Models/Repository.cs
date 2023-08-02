using HastaneRandevuSistemi.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HastaneRandevuSistemi.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;

        public Repository(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
            this.dbSet = _uygulamaDbContext.Set<T>();
            _uygulamaDbContext.Doktorlar.Include(k => k.DoktorBrans).Include(k => k.DoktorBransId);
        }
        public void Ekle(T entitiy)
        {
            
            dbSet.Add(entitiy);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);//bu metotta bir tane eleman getirmesini istiyoruz ama bu kod bunu garanti etmez o yüzden aşağıdaki kalıbı yazıyoruz.

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(prop);
                }
            }
#pragma warning disable CS8603 // Olası null başvuru dönüşü.
            return sorgu.FirstOrDefault();
#pragma warning restore CS8603 // Olası null başvuru dönüşü.
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(prop);
                }
            }
            return sorgu.ToList();
        }

        public void Sil(T entitiy)
        {
            
            dbSet.Remove(entitiy);
        }

        public void SilAralik(IEnumerable<T> entities)//birden fazla eleman silme işlemi
        {
            dbSet.RemoveRange(entities);
        }
    }
}
