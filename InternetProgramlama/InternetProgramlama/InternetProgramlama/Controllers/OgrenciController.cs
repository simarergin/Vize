using InternetProgramlama.Models;
using InternetProgramlama.data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetProgramlama.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ILogger<OgrenciController> _logger;
        private OkulIdareContext _okulIdareContext;

        public OgrenciController(ILogger<OgrenciController> logger, OkulIdareContext okulIdareContext)
        {
            _logger = logger;
            _okulIdareContext = okulIdareContext;
        }

        public IActionResult Index()
        {
            OgrenciModel ogrenciModel = new OgrenciModel();
            ogrenciModel.OgrenciList = new List<Ogrenci>();
            var data = _okulIdareContext.Ogrencis.ToList();
            foreach (var item in data)
            {
                ogrenciModel.OgrenciList.Add(new Ogrenci
                {
                    Id = item.Id,
                    AdSoyad = item.AdSoyad,
                    KayitTarih = item.KayitTarih,
                    OgrenciNo = item.OgrenciNo,
                    DTarih = item.DTarih,
                    Bolum = item.Bolum

                });
            }

            return View(ogrenciModel);
        }

        [HttpGet]
        public IActionResult Save()
        {
            Ogrenci ogrenci = new Ogrenci();
            return View(ogrenci);
        }
        [HttpPost]
        public IActionResult Save(Ogrenci ogrenci)
        {
            try
            {
                var ogrenciveri = new Ogrenci()
                {
                    Id = ogrenci.Id,
                    AdSoyad = ogrenci.AdSoyad,
                    KayitTarih = ogrenci.KayitTarih,
                    OgrenciNo = ogrenci.OgrenciNo,
                    Dtarih = ogrenci.DTarih,
                    Bolum = ogrenci.Bolum
                };
                _okulIdareContext.Ogrencis.Add(ogrenciveri);
                _okulIdareContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "Ogrenci");
        }
        [HttpGet]
        public IActionResult Update(int Id = 0)
        {
            Ogrenci ogrenci = new Ogrenci();
            var data = _okulIdareContext.Ogrencis.Where(m => m.Id == Id).FirstOrDefault();
            if (data != null)
            {
                ogrenci.Id = data.Id;
                ogrenci.AdSoyad = data.AdSoyad;
                ogrenci.KayitTarih = data.KayitTarih;
                ogrenci.OgrenciNo = data.OgrenciNo;
                ogrenci.Dtarih = data.DTarih;
                ogrenci.Bolum = data.Bolum;
            }
            return View(ogrenci);
        }
        [HttpPost]
        public IActionResult Update(Ogrenci ogrenci)
        {
            try
            {
                var data = _okulIdareContext.Ogrencis.Where(m => m.Id == ogrenci.Id).FirstOrDefault();
                data.AdSoyad = ogrenci.AdSoyad;
                data.KayitTarih = ogrenci.KayitTarih;
                data.OgrenciNo = ogrenci.OgrenciNo;
                data.DTarih = ogrenci.Dtarih;
                data.Bolum = ogrenci.Bolum;
                _okulIdareContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "Ogrenci");
        }
        [HttpGet]
        public IActionResult Delete(int Id = 0)
        {
            try
            {
                var data = _okulIdareContext.Ogrencis.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _okulIdareContext.Ogrencis.Remove(data);
                    _okulIdareContext.SaveChanges();
                }
                TempData["DeleteStatus"] = 1;

            }
            catch
            {
                TempData["DeleteStatus"] = 0;
            }
            return RedirectToAction("Index", "Ogrenci");
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