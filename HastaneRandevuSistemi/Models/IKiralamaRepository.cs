namespace HastaneRandevuSistemi.Models
{
    public interface IKiralamaRepository : IRepository<Kiralama> //extends
    {
        void Guncelle(Kiralama kiralamaInterface);
        void Kaydet();
    }
}
