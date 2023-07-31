using asp_udemy_proje1.Models;
using Microsoft.AspNetCore.Authorization;
using asp_udemy_proje1.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace asp_udemy_proje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]//bütün controller dosyasında auth istersen buraya,
                                             //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class KitapTuruController : Controller
    {

        private readonly IKitapTuruRepository _kitapTuruRepository;

        public KitapTuruController(IKitapTuruRepository context)
        {
            _kitapTuruRepository = context;
        }


        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();

            return View(objKitapTuruList);
        }




        public IActionResult Ekle()
        {
            //List<KitapTuru> objKitapTuruList = _uygulamaDbContext.KitapTurleri.ToList();

            return View();
        }




        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult Ekle(KitapTuru kitapTuruEkle)
        {
            if(ModelState.IsValid) {
                _kitapTuruRepository.Ekle(kitapTuruEkle);
                _kitapTuruRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.
                
                    TempData["basarili"] = "Kitap Türü ekleme işlemi başarılı!";
               
                return RedirectToAction("Index", "KitapTuru");//kaydolunca seni KitapTuru kontrolörünün index action metodunun viewine gönderir.
            }
            else
            {
                return View();
            }
            

        }

















        public IActionResult Guncelle(int? id)
        {


            if (id == null || id == 0){ 
            
                return NotFound();
            
            }



            // KitapTuru? kitapTuruDb = _uygulamaDbContext.KitapTurleri.Find(id); eski kullanım

            KitapTuru? kitapTuruDb = _kitapTuruRepository.Get(u=>u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kitapTuruDb == null){
            

                return NotFound();

            }


            return View(kitapTuruDb);
        }












        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult Guncelle(KitapTuru kitapTuruGuncelle)   
        {


            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuruGuncelle);
                _kitapTuruRepository.Kaydet();
               
                    TempData["basarili"] = "Kitap Türü güncelleme işlemi başarılı!";
                
                return RedirectToAction("Index", "KitapTuru");//güncelleyince seni KitapTuru kontrolörünün index action metodunun viewine gönderir.
            }
            else{

                return View();
            }
        }



















        public IActionResult Sil(int? id)
        {


            if (id == null || id == 0)
            {

                return NotFound();

            }



            KitapTuru? kitapTuruDb = _kitapTuruRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kitapTuruDb == null)
            {


                return NotFound();

            }


            return View(kitapTuruDb);
        }














        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult SilPOST(int? id)
        {


            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kitapTuru == null)
            {
                return NotFound();
            }
            else
            {
                _kitapTuruRepository.Sil(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kitap Türü silme işlemi başarılı!";
                return RedirectToAction("Index", "KitapTuru");//güncelleyince seni KitapTuru kontrolörünün index action metodunun viewine gönderir.
            }
        }

    }
}
