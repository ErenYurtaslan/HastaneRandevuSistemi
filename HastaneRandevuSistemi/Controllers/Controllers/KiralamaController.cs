using asp_udemy_proje1.Models;
using asp_udemy_proje1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;

namespace asp_udemy_proje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]//bütün controller dosyasında auth istersen buraya,
                                             //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class KiralamaController : Controller
    {

        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
           // List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();

            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();

            return View(objKiralamaList);
        }




        public IActionResult EkleGuncelle(int? id)
        {
            //List<Kitap> objKitapList = _uygulamaDbContext.Kitaplar.ToList();

            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.KitapList = KitapList;


            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else{//güncelle

                 // Kitap? kitapDb =  _uygulamaDbContext.Kitaplar.Find(id); eski kullanım

                Kiralama? kiralamaDb = _kiralamaRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (kiralamaDb == null)
                {


                    return NotFound();

                }
                return View(kiralamaDb);
            }
        }




        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {

            if(ModelState.IsValid) {

                


                if (kiralama.Id == 0) {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Yeni kiralama kaydedildi!";
                }
                else {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Kiralama güncellendi!";
                }

                _kiralamaRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.
                
                
               
                return RedirectToAction("Index", "Kiralama");//kaydolunca seni Kitap kontrolörünün index action metodunun viewine gönderir.
            }
            else
            {
                return View();
            }
            

        }











        public IActionResult Sil(int? id)
        {

            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.KitapList = KitapList;


            if (id == null || id == 0)
            {

                return NotFound();

            }



            Kiralama? kiralamaDb = _kiralamaRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kiralamaDb == null)
            {


                return NotFound();

            }


            return View(kiralamaDb);
        }














        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult SilPOST(int? id)
        {


            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kiralama == null)
            {
                return NotFound();
            }
            else
            {
                _kiralamaRepository.Sil(kiralama);
                _kiralamaRepository.Kaydet();
                TempData["basarili"] = "Kiralama silindi!";
                return RedirectToAction("Index", "Kiralama");//güncelleyince seni Kitap kontrolörünün index action metodunun viewine gönderir.
            }
        }

    }
}
