using HastaneRandevuSistemi.Models;
using HastaneRandevuSistemi.Services;
using HastaneRandevuSistemi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing;

namespace HastaneRandevuSistemi.Controllers
{
    /// <summary>
    ///[Authorize(Roles =UserRoles.Role_Admin)]//bütün controller dosyasında auth istersen buraya,
    /// </summary>
    //spesifik actionlar için istersen aşağıda istediğin metodun başına bunu yazıyorsun 
    public class DoktorController : Controller
    {

        private readonly IDoktorRepository _doktorRepository;
        private readonly IDoktorBransRepository _doktorBransRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LanguageService _localization;

        public DoktorController(IDoktorRepository doktorRepository, IDoktorBransRepository doktorBransRepository, IWebHostEnvironment webHostEnvironment, LanguageService localization)
        {
            _doktorRepository = doktorRepository;
            _doktorBransRepository = doktorBransRepository;
            _webHostEnvironment = webHostEnvironment;
            _localization = localization;   
        }

       [Authorize(Roles = "Admin , Hasta")]
        public IActionResult Index()
        {


            List<Doktor> objDoktorList = _doktorRepository.GetAll(includeProps: "DoktorBrans").ToList();

            return View(objDoktorList);
        }



       [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {


            IEnumerable<SelectListItem> DoktorBransList = _doktorBransRepository.GetAll().
                Select(
                k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString(),
                }
               );

            ViewBag.DoktorBransList = DoktorBransList;
            if (id == null || id == 0)//ekle
            {

                return View();

            }
            else
            {//güncelle



                Doktor? doktorDb = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

                if (doktorDb == null)
                {


                    return NotFound();

                }
                return View(doktorDb);
            }
        }





        [HttpPost]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Doktor doktor, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string doktorPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(doktorPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    doktor.ResimUrl = @"\img\" + file.FileName;
                }



                if (doktor.Id == 0)
                {
                    _doktorRepository.Ekle(doktor);
                    TempData["basarili"] = @_localization.Getkey("Doktor kayıt ekleme işlemi başarılı!").Value;
                }
                else
                {
                    _doktorRepository.Guncelle(doktor);
                    TempData["basarili"] = @_localization.Getkey("Doktor kayıt güncelleme işlemi başarılı!").Value;
                }

                _doktorRepository.Kaydet();//bunu yapmazsan db'ye bilgiler eklenmez.



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
            if (id == null || id == 0)
            {

                return NotFound();

            }



            Doktor? doktorDb = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktorDb == null)
            {


                return NotFound();

            }


            return View(doktorDb);
        }














        [HttpPost, ActionName("Sil")]//bunu yazmazsan yukarıda aynı isimle action olduğu için "catch" çalışır, sayfayı göremezsin.
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {


            Doktor? doktor = _doktorRepository.Get(u => u.Id == id);//Expression<Func<T, bool>> filtre kısmı

            if (doktor == null)
            {
                return NotFound();
            }
            else
            {
                _doktorRepository.Sil(doktor);
                _doktorRepository.Kaydet();
                TempData["basarili"] = @_localization.Getkey("Doktor kayıt silme işlemi başarılı!").Value;
                return RedirectToAction("Index", "Doktor");
            }

        }
    }
}
