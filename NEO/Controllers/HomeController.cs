using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEO.Code;
using NEO.Code.Model;

namespace NEO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AsteroidModel asteroid)
        {
            var ac = new AsteroidsClassification();
            ac.LoadMachine(false);

            asteroid.Classification = "";
            ViewBag.result = ac.Predict(asteroid);

            return View(asteroid);
        }

        public ActionResult Import()
        {
            var importData = new ImportData();
            importData.Start();

            return View();
        }

        public ActionResult Learn()
        {
            var asc = new AsteroidsClassification();
            asc.LoadMachine(false);

            return View();
        }
    }
}