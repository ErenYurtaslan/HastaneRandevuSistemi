namespace HastaneRandevuSistemi.Models
{
    public interface IKitapRepository : IRepository<Kitap> //extends
    {
        void Guncelle(Kitap kitapInterface);
        void Kaydet();
    }
}
