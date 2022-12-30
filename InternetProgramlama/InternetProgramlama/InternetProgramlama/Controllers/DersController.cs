using InternetProgramlama.Models;
using Microsoft.AspNetCore.Mvc;
using InternetProgramlama.data;
using System.Diagnostics;

namespace InternetProgramlama.Controllers
{
    public class DersController : Controller
    {
        private readonly ILogger<DersController> _logger;
        private OkulIdareContext _okulIdareContext;

        public DersController(ILogger<DersController> logger, OkulIdareContext okulIdareContext)
        {
            _logger = logger;
            _okulIdareContext = okulIdareContext;
        }

        public IActionResult Index()
        {
            DersModel dersModel = new DersModel();
            dersModel.DersList = new List<Ders>();
            var data = _okulIdareContext.Ders.ToList();
            foreach (var item in data)
            {
                dersModel.DersList.Add(new Ders
                {
                    Id = item.Id,
                    Ad = item.Ad,
                    Kredisi = item.Kredisi,
                    OkulYonetimId = item.OkulYonetimId
                });
            }

            return View(dersModel);
        }

        [HttpGet]
        public IActionResult Save()
        {
            Ders ders = new Ders();
            return View(ders);
        }
        [HttpPost]
        public IActionResult Save(Ders ders)
        {
            try
            {
                var dersVeri = new Der()
                {
                    Id = ders.Id,
                    Ad = ders.Ad,
                    Kredisi = (int)ders.Kredisi,
                    OkulYonetimId = (int)ders.OkulYonetimId

                };
                _okulIdareContext.Ders.Add(dersVeri);
                _okulIdareContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "Ders");
        }
        [HttpGet]
        public IActionResult Update(int Id = 0)
        {
            Ders ders = new Ders();
            var data = _okulIdareContext.Ders.Where(m => m.Id == Id).FirstOrDefault();
            if (data != null)
            {
                ders.Id = data.Id;
                ders.Ad = data.Ad;
                ders.Kredisi = data.Kredisi;
                ders.OkulYonetimId = data.OkulYonetimId;
            }
            return View(ders);
        }
        [HttpPost]
        public IActionResult Update(Ders ders)
        {
            try
            {
                var data = _okulIdareContext.Ders.Where(m => m.Id == ders.Id).FirstOrDefault();
                ders.Ad = ders.Ad;
                ders.Kredisi = ders.Kredisi;
                data.OkulYonetimId = (int)ders.OkulYonetimId;

                _okulIdareContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "Ders");
        }
        [HttpGet]
        public IActionResult Delete(int Id = 0)
        {
            try
            {
                var data = _okulIdareContext.Ders.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _okulIdareContext.Ders.Remove(data);
                    _okulIdareContext.SaveChanges();
                }
                TempData["DeleteStatus"] = 1;

            }
            catch
            {
                TempData["DeleteStatus"] = 0;
            }
            return RedirectToAction("Index", "Ders");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}