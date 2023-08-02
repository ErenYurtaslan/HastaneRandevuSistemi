namespace HastaneRandevuSistemi.Models
{
    public interface IKitapTuruRepository : IRepository<KitapTuru> //extends
    {
        void Guncelle(KitapTuru kitapTuruInterface);
        void Kaydet();
    }
}
