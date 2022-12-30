using InternetProgramlama.data;
using InternetProgramlama.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetProgramlama.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ILogger<OgrenciController> _logger;
        private IdareDBContext _ıdareDBContext;

        public OgrenciController(ILogger<OgrenciController> logger, IdareDBContext ıdareDBContext)
        {
            _logger = logger;
            _ıdareDBContext = ıdareDBContext;
        }

        public IActionResult Index()
        {
            OgrenciModel ogrenciModel = new OgrenciModel();
            ogrenciModel.OgrenciList = new List<Ogrenci>();
            var data = _ıdareDBContext.OgrenciTablos.ToList();
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
        public IActionResult Kaydet()
        {
            Ogrenci ogrenci = new Ogrenci();
            return View(ogrenci);
        }
        [HttpPost]
        public IActionResult Kaydet(Ogrenci ogrenci)
        {
            try
            {
                var ogrenciveri = new OgrenciTablo()
                {
                    Id = ogrenci.Id,
                    AdSoyad = ogrenci.AdSoyad,
                    KayitTarih = ogrenci.KayitTarih,
                    OgrenciNo = ogrenci.OgrenciNo,
                    DTarih = ogrenci.DTarih,
                    Bolum = ogrenci.Bolum
                };
                _ıdareDBContext.OgrenciTablos.Add(ogrenciveri);
                _ıdareDBContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "Ogrenci");
        }
        [HttpGet]
        public IActionResult Guncelle(int Id = 0)
        {
            Ogrenci ogrenci = new Ogrenci();
            var data = _ıdareDBContext.OgrenciTablos.Where(m => m.Id == Id).FirstOrDefault();
            if (data != null)
            {
                ogrenci.Id = data.Id;
                ogrenci.AdSoyad = data.AdSoyad;
                ogrenci.KayitTarih = data.KayitTarih;
                ogrenci.OgrenciNo = data.OgrenciNo;
                ogrenci.DTarih = data.DTarih;
                ogrenci.Bolum = data.Bolum;
            }
            return View(ogrenci);
        }
        [HttpPost]
        public IActionResult Guncelle(Ogrenci ogrenci)
        {
            try
            {
                var data = _ıdareDBContext.OgrenciTablos.Where(m => m.Id == ogrenci.Id).FirstOrDefault();
                data.AdSoyad = ogrenci.AdSoyad;
                data.KayitTarih = ogrenci.KayitTarih;
                data.OgrenciNo = ogrenci.OgrenciNo;
                data.DTarih = ogrenci.DTarih;
                data.Bolum = ogrenci.Bolum;
                _ıdareDBContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "Ogrenci");
        }
        [HttpGet]
        public IActionResult Sil(int Id = 0)
        {
            try
            {
                var data = _ıdareDBContext.OgrenciTablos.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _ıdareDBContext.OgrenciTablos.Remove(data);
                    _ıdareDBContext.SaveChanges();
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