using System.Drawing;

namespace HastaneRandevuSistemi.Models
{
    public interface IDoktorRepository : IRepository<Doktor> //extends
    {
        void Guncelle(Doktor doktorInterface);
        void Kaydet();
    }
}
