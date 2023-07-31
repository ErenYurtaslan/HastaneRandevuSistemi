namespace HastaneRandevuSistemi.Models
{
    public interface IDoktorBransRepository : IRepository<DoktorBrans> //extends
    {
        void Guncelle(DoktorBrans doktorBransInterface);
        void Kaydet();
    }
}
