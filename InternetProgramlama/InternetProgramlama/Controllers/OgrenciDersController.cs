using InternetProgramlama.data;
using InternetProgramlama.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OkulProje.Controllers
{
    public class OgrenciDersController : Controller
    {
        private readonly ILogger<OgrenciDersController> _logger;
        private IdareDBContext _ıdareDBContext;

        public OgrenciDersController(ILogger<OgrenciDersController> logger, IdareDBContext ıdareDBContext)
        {
            _logger = logger;
            _ıdareDBContext = ıdareDBContext;
        }

        public IActionResult Index()
        {
            OgrenciDersModel ogrenciDersModel = new OgrenciDersModel();
            ogrenciDersModel.OgrenciDersList = new List<OgrenciDers>();
            var data = _ıdareDBContext.OgrenciDersTablos.ToList();
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
                var ogrenciDersveri = new OgrenciDersTablo()
                {
                    Id = ogrenciDers.Id,
                    DersId = ogrenciDers.DersId,
                    OgrenciId = ogrenciDers.Id
                };
                _ıdareDBContext.OgrenciDersTablos.Add(ogrenciDersveri);
                _ıdareDBContext.SaveChanges();
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
            var data = _ıdareDBContext.OgrenciDersTablos.Where(m => m.Id == Id).FirstOrDefault();
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
                var data = _ıdareDBContext.OgrenciDersTablos.Where(m => m.Id == ogrenciDers.Id).FirstOrDefault();
                data.DersId = ogrenciDers.DersId;
                data.OgrenciId = ogrenciDers.Id;
                _ıdareDBContext.SaveChanges();
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
                var data = _ıdareDBContext.OgrenciDersTablos.Where(m => m.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    _ıdareDBContext.OgrenciDersTablos.Remove(data);
                    _ıdareDBContext.SaveChanges();
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