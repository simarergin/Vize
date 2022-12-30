using InternetProgramlama.Models;
using InternetProgramlama.data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetProgramlama.Controllers
{
    public class OgrenciDersController : Controller
    {
        private readonly ILogger<OgrenciDersController> _logger;
        private OkulIdareContext _okulIdareContext;

        public OgrenciDersController(ILogger<OgrenciDersController> logger, OkulIdareContext okulIdareContext)
        {
            _logger = logger;
            _okulIdareContext = okulIdareContext;
        }

        public IActionResult Index()
        {
            OgrenciDersModel ogrenciDersModel = new OgrenciDersModel();
            ogrenciDersModel.OgrenciDersList = new List<OkulDer>();
            var data = _okulIdareContext.OgrenciDer.ToList();
            foreach (var item in data)
            {
                ogrenciDersModel.OgrenciDersList.Add(new OgrenciDers
                {
                    Id = item.Id,
                    DersId = item.Id,
                    OgrenciId = item.Id

                });
            }

            return View(ogrenciDersModel);
        }

        [HttpGet]
        public IActionResult Kaydet()
        {
            OgrenciDers ogrenciDers = new OgrenciDers();
            return View(ogrenciDers);
        }
        [HttpPost]
        public IActionResult Kaydet(OgrenciDers ogrenciDers)
        {
            try
            {
                var ogrenciDersveri = new OgrenciDer()
                {
                    Id = ogrenciDers.Id,
                    DersId = ogrenciDers.DersId,
                    OgrenciId = ogrenciDers.Id
                };
                _okulIdareContext.OgrenciDers.Add(ogrenciDersveri);
                _okulIdareContext.SaveChanges();
                TempData["SaveStatus"] = 1;
            }
            catch (Exception ex)
            {
                TempData["SaveStatus"] = 0;
            }
            return RedirectToAction("Index", "OgrenciDers");
        }
        [HttpGet]
        public IActionResult Guncelle(int Id = 0)
        {
            OgrenciDers ogrenciDers = new OgrenciDers();
            var data = _okulIdareContext.OgrenciDers.Where(m => m.Id == Id).FirstOrDefault();
            if (data != null)
            {
                ogrenciDers.Id = data.Id;
                ogrenciDers.DersId = data.Id;
                ogrenciDers.OgrenciId = data.Id;
            }
            return View(ogrenciDers);
        }
        [HttpPost]
        public IActionResult Guncelle(OgrenciDers ogrenciDers)
        {
            try
            {
                var data = _okulIdareContext.OgrenciDers.Where(m => m.Id == ogrenciDers.Id).FirstOrDefault();
                data.DersId = ogrenciDers.DersId;
                data.OgrenciId = ogrenciDers.Id;
                _okulIdareContext.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch
            {
                TempData["UpdateStatus"] = 0;
            }
            return RedirectToAction("Index", "OgrenciDers");
        }
        [HttpGet]
        public IActionResult Sil(int Id = 0)
        {
            try
            {
                var data = _okulIdareContext.OgrenciDers.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _okulIdareContext.OgrenciDers.Remove(data);
                    _okulIdareContext.SaveChanges();
                }
                TempData["DeleteStatus"] = 1;

            }
            catch
            {
                TempData["DeleteStatus"] = 0;
            }
            return RedirectToAction("Index", "OgrenciDers");
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