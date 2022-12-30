using InternetProgramlama.Models;
using InternetProgramlama.data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetProgramlama.Controllers
{
    public class OkulYonetimController : Controller
    {
        private readonly ILogger<OkulYonetimController> _logger;
        private OkulIdareContext _okulIdareContext;

        public OkulYonetimController(ILogger<OkulYonetimController> logger, OkulIdareContext okulIdareContext)
        {
            _logger = logger;
            _okulIdareContext = okulIdareContext;
        }

        public IActionResult Index()
        {
            OkulYonetimModel okulYonetimModel = new OkulYonetimModel();
            okulYonetimModel.OkulYonetimList = new List<Models.OkulYonetim>();
            var data = _okulIdareContext.OkulYonetims.ToList();
            foreach (var item in data)
            {
                okulYonetimModel.OkulYonetimList.Add(new Models.OkulYonetim
                {
                    Id = item.Id,
                    AdSoyad = item.AdSoyad,
                    Gorevi = item.Gorevi,
                    YonetimTip = item.YonetimTip
                });
            }

            return View(okulYonetimModel);
        }

        [HttpGet]
        public IActionResult Kaydet()
        {
            data.OkulYonetim okulYonetim = new data.OkulYonetim();
            return View(okulYonetim);
        }
        [HttpPost]
        public IActionResult Kaydet(data.OkulYonetim okulYonetim)
        {
            try
            {
                var okulYonetimVeri = new data.OkulYonetim()
                {
                    Id = okulYonetim.Id,
                    AdSoyad = okulYonetim.AdSoyad,
                    Gorevi = okulYonetim.Gorevi,
                    YonetimTip = okulYonetim.YonetimTip

                };
                _okulIdareContext.OkulYonetims.Add(okulYonetimVeri);
                _okulIdareContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "OkulYonetim");
        }
        [HttpGet]
        public IActionResult Guncelle(int Id = 0)
        {
            data.OkulYonetim okulYonetim = new data.OkulYonetim();
            var data = _okulIdareContext.OkulYonetims.Where(m => m.Id == Id).FirstOrDefault();
            if (data != null)
            {
                okulYonetim.Id = data.Id;
                okulYonetim.AdSoyad = data.AdSoyad;
                okulYonetim.Gorevi = data.Gorevi;
                okulYonetim.YonetimTip = data.YonetimTip;
            }
            return View(okulYonetim);
        }
        [HttpPost]
        public IActionResult Guncelle(data.OkulYonetim okulYonetim)
        {
            try
            {
                var data = _okulIdareContext.OkulYonetims.Where(m => m.Id == okulYonetim.Id).FirstOrDefault();
                data.AdSoyad = okulYonetim.AdSoyad;
                data.Gorevi = okulYonetim.Gorevi;
                data.YonetimTip = okulYonetim.YonetimTip;

                _okulIdareContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "OkulYonetim");
        }
        [HttpGet]
        public IActionResult Sil(int Id = 0)
        {
            try
            {
                var data = _okulIdareContext.OkulYonetims.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _okulIdareContext.OkulYonetims.Remove(data);
                    _okulIdareContext.SaveChanges();
                }
                TempData["DeleteStatus"] = 1;

            }
            catch
            {
                TempData["DeleteStatus"] = 0;
            }
            return RedirectToAction("Index", "OkulYonetim");
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