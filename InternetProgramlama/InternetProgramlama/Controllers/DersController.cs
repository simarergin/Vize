using InternetProgramlama.data;
using InternetProgramlama.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OkulProje.Controllers
{
    public class DersController : Controller
    {
        private readonly ILogger<DersController> _logger;
        private IdareDBContext _ıdareDBContext;

        public DersController(ILogger<DersController> logger, IdareDBContext ıdareDBContext)
        {
            _logger = logger;
            _ıdareDBContext = ıdareDBContext;
        }

        public IActionResult Index()
        {
            DersModel dersModel = new DersModel();
            dersModel.DersList = new List<Ders>();
            var data = _ıdareDBContext.DersTablos.ToList();
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
        public IActionResult Kaydet()
        {
            Ders ders = new Ders();
            return View(ders);
        }
        [HttpPost]
        public IActionResult Kaydet(Ders ders)
        {
            try
            {
                var dersVeri = new DersTablo()
                {
                    Id = ders.Id,
                    Ad = ders.Ad,
                    Kredisi = ders.Kredisi,
                    OkulYonetimId = ders.OkulYonetimId

                };
                _ıdareDBContext.DersTablos.Add(dersVeri);
                _ıdareDBContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "Ders");
        }
        [HttpGet]
        public IActionResult Guncelle(int Id = 0)
        {
            Ders ders = new Ders();
            var data = _ıdareDBContext.DersTablos.Where(m => m.Id == Id).FirstOrDefault();
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
        public IActionResult Guncelle(Ders ders)
        {
            try
            {
                var data = _ıdareDBContext.DersTablos.Where(m => m.Id == ders.Id).FirstOrDefault();
                ders.Ad = ders.Ad;
                ders.Kredisi = ders.Kredisi;
                data.OkulYonetimId = ders.OkulYonetimId;

                _ıdareDBContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "Ders");
        }
        [HttpGet]
        public IActionResult Sil(int Id = 0)
        {
            try
            {
                var data = _ıdareDBContext.DersTablos.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _ıdareDBContext.DersTablos.Remove(data);
                    _ıdareDBContext.SaveChanges();
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