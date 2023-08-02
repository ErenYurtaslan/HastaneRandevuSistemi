using HastaneRandevuSistemi.Models;
using HastaneRandevuSistemi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HastaneRandevuSistemi.Controllers
{
    //[Authorize(Roles = UserRoles.Role_Admin)] bütün controller dosyasında auth istersen buraya,
    //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class RandevuController : Controller
    {

        private readonly IRandevuRepository _randevuRepository;
        private readonly IDoktorRepository _doktorRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public RandevuController(IRandevuRepository randevuRepository, IDoktorRepository doktorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _randevuRepository = randevuRepository;
            _doktorRepository = doktorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Index()
        {
            

            List<Randevu> objRandevuList = _randevuRepository.GetAll(includeProps: "Doktor").ToList();

            return View(objRandevuList);
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
           
            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.DoktorAdi,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorList = DoktorList;


            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else
            {//güncelle

                

                Randevu? randevuDb = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (randevuDb == null)
                {


                    return NotFound();

                }
                return View(randevuDb);
            }
        }



        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult EkleGuncelle(Randevu randevu)
        {

            if (ModelState.IsValid)
            {


                if (randevu.Id == 0)
                {
                    _randevuRepository.Ekle(randevu);
                    TempData["basarili"] = "Yeni randevu kaydedildi!";
                }
                else
                {
                    _randevuRepository.Guncelle(randevu);
                    TempData["basarili"] = "Randevu güncellendi!";
                }

                _randevuRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



                return RedirectToAction("Index", "Randevu");
            }
            else
            {
                return View();
            }


        }






        [Authorize(Roles = UserRoles.Role_Hasta)]
        public IActionResult RandevuAl(int? id)
        {

            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.DoktorAdi,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorList = DoktorList;


           // if (id == null || id == 0)//ekle
            //{

                return View();

           // }
           
        }


        [Authorize(Roles = UserRoles.Role_Hasta)]
        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult RandevuAl(Randevu randevu)
        {

            if (ModelState.IsValid)
            {

                randevu.Id = 0;


               _randevuRepository.Ekle(randevu);
                TempData["basarili1"] = "Yeni randevu alındı!";
               
                    

                _randevuRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



                return RedirectToAction("Index", "Doktor");
            }
            else
            {
                return View();
            }


        }






        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {

            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.DoktorAdi,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorList = DoktorList;


            if (id == null || id == 0)
            {

                return NotFound();

            }



            Randevu? randevuDb = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (randevuDb == null)
            {


                return NotFound();

            }


            return View(randevuDb);
        }













        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult SilPOST(int? id)
        {


            Randevu? randevu = _randevuRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (randevu == null)
            {
                return NotFound();
            }
            else
            {
                _randevuRepository.Sil(randevu);
                _randevuRepository.Kaydet();
                TempData["basarili"] = "Randevu silindi!";
                return RedirectToAction("Index", "Randevu");
            }
        }

    }
}
