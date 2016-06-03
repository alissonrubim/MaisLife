using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Parceiros()
        {
            ViewBag.Patners = ConfigDB.Model.Parceiros.ToList();
            return View();
        }

        public ActionResult RemoverParceiro()
        {
            var count = Convert.ToInt32(Request.Form["count"]);
            for (var i = 1; i <= count; i++ )
            {
                var id = Convert.ToInt32(Request.Form["item-" + i]);
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
                ConfigDB.Model.Delete(patner);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();

            return RedirectToAction("Parceiros");
        }
    }
}