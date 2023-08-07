using HastaneRandevuSistemi.Utility;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using HastaneRandevuSistemi.Services;

namespace HastaneRandevuSistemi.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]//bütün controller dosyasında auth istersen buraya,
                                             //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class DoktorBransController : Controller
    {
        private readonly IDoktorBransRepository _doktorBransRepository;
        private readonly LanguageService _localization;

        public DoktorBransController(IDoktorBransRepository context, LanguageService localization)
        {
            _doktorBransRepository = context;
            _localization = localization;
        }


        public IActionResult Index()
        {
            List<DoktorBrans> objDoktorBransList = _doktorBransRepository.GetAll().ToList();

            return View(objDoktorBransList);
        }




        public IActionResult Ekle()
        {

            return View();
        }




        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult Ekle(DoktorBrans doktorBransEkle)
        {
            if (ModelState.IsValid)
            {
                _doktorBransRepository.Ekle(doktorBransEkle);
                _doktorBransRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.

                TempData["basarili"] = @_localization.Getkey("Doktor Branşı ekleme işlemi başarılı!").Value;

                return RedirectToAction("Index", "DoktorBrans");
            }
            else
            {
                return View();
            }


        }

















        public IActionResult Guncelle(int? id)
        {


            if (id == null || id == 0)
            {

                return NotFound();

            }



           

            DoktorBrans? doktorBransDb= _doktorBransRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktorBransDb == null)
            {


                return NotFound();

            }


            return View(doktorBransDb);
        }












        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult Guncelle(DoktorBrans doktorBransGuncelle)
        {


            if (ModelState.IsValid)
            {
                _doktorBransRepository.Guncelle(doktorBransGuncelle);
                _doktorBransRepository.Kaydet();

                TempData["basarili"] = @_localization.Getkey("Doktor Branşı güncelleme işlemi başarılı!").Value;

                return RedirectToAction("Index", "DoktorBrans");
            }
            else
            {

                return View();
            }
        }



















        public IActionResult Sil(int? id)
        {


            if (id == null || id == 0)
            {

                return NotFound();

            }



            DoktorBrans? doktorBransDb = _doktorBransRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktorBransDb == null)
            {


                return NotFound();

            }


            return View(doktorBransDb);
        }














        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        public IActionResult SilPOST(int? id)
        {


            DoktorBrans? doktorBrans = _doktorBransRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktorBrans == null)
            {
                return NotFound();
            }
            else
            {
                _doktorBransRepository.Sil(doktorBrans);
                _doktorBransRepository.Kaydet();
                TempData["basarili"] = @_localization.Getkey("Doktor Branşı silme işlemi başarılı!").Value;
                return RedirectToAction("Index", "DoktorBrans");
            }
        }
    }
}
