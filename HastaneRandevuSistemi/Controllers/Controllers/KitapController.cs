using asp_udemy_proje1.Models;
using asp_udemy_proje1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace asp_udemy_proje1.Controllers
{
   /// <summary>
   ///[Authorize(Roles =UserRoles.Role_Admin)]//bütün controller dosyasında auth istersen buraya,
   /// </summary>
                                            //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class KitapController : Controller
    {

        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles ="Admin , Ogrenci")]
        public IActionResult Index()
        {
           // List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();

            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();

            return View(objKitapList);
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            //List<Kitap> objKitapList = _uygulamaDbContext.Kitaplar.ToList();

            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString(),//yeni eklenen kitabın türüne kitap türünün idsini yazınca, kaydedilenlerde yeni eklenen kitabın türü isim olarak yazacak.
                }
               );

            ViewBag.KitapTuruList = KitapTuruList;
            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else{//güncelle

                 // Kitap? kitapDb =  _uygulamaDbContext.Kitaplar.Find(id); eski kullanım

                Kitap? kitapDb = _kitapRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (kitapDb == null)
                {


                    return NotFound();

                }
                return View(kitapDb);
            }
        }




       
        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if(ModelState.IsValid) {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

              if(file != null)
              {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                    file.CopyTo(fileStream);
                    }

                    kitap.ResimUrl = @"\img\" + file.FileName;
              }  
                


                if (kitap.Id == 0) {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Kitap ekleme işlemi başarılı!";
                }
                else {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap güncelleme işlemi başarılı!";
                }
                
                _kitapRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.
                
                
               
                return RedirectToAction("Index", "Kitap");//kaydolunca seni Kitap kontrolörünün index action metodunun viewine gönderir.
            }
            else
            {
                return View();
            }
            

        }
















        /*
                public IActionResult Guncelle(int? id)
                {


                    if (id == null || id == 0){ 

                        return NotFound();

                    }



                    // Kitap? kitapDb =  _uygulamaDbContext.Kitaplar.Find(id); eski kullanım

                    Kitap? kitapDb = _kitapRepository.Get(u=>u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                    if (kitapDb == null){


                        return NotFound();

                    }


                    return View(kitapDb);
                }
        */









        /*

                [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
                public IActionResult Guncelle(Kitap kitapGuncelle)   
                {


                    if (ModelState.IsValid)
                    {
                        _kitapRepository.Guncelle(kitapGuncelle);
                        _kitapRepository.Kaydet();

                            TempData["basarili"] = "Kitap güncelleme işlemi başarılı!";

                        return RedirectToAction("Index", "Kitap");//güncelleyince seni Kitap kontrolörünün index action metodunun viewine gönderir.
                    }
                    else{

                        return View();
                    }
                }

                */
















        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {


            if (id == null || id == 0)
            {

                return NotFound();

            }



            Kitap? kitapDb = _kitapRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kitapDb == null)
            {


                return NotFound();

            }


            return View(kitapDb);
        }














        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {


            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (kitap == null)
            {
                return NotFound();
            }
            else
            {
                _kitapRepository.Sil(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Kitap silme işlemi başarılı!";
                return RedirectToAction("Index", "Kitap");//güncelleyince seni Kitap kontrolörünün index action metodunun viewine gönderir.
            }
        }

    }
}
