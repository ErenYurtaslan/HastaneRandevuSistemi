namespace HastaneRandevuSistemi.Models
{
    public interface IRandevuRepository : IRepository<Randevu> //extends
    {
        void Guncelle(Randevu randevuInterface);
        void Kaydet();
    }
}
