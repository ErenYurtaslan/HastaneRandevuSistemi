using System.Linq.Expressions;

namespace HastaneRandevuSistemi.Models
{
    public interface IRepository<T> where T : class
    {
        //T -> DoktorBrans

        IEnumerable<T> GetAll(string? includeProps = null);

        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);

        void Ekle(T entitiy);

        void Sil(T entitiy);

        void SilAralik(IEnumerable<T> entities);
    }
}
