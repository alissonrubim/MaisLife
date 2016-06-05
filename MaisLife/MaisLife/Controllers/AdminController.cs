using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaisLife.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
  
        public ActionResult Parceiros(ParceiroAdapter patner = null)
        {
            ViewBag.Patners = ConfigDB.Model.Parceiros.ToList();
            if ( patner != null )
                return View(patner);
            else
                return View();
        }

        [HttpPost]
        public ActionResult ManagerPatner(ParceiroAdapter adapter, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Uploads"), file.FileName);
                    file.SaveAs(path);
                    adapter.Imagem = path;
                }           
            }
             

            if (adapter.Id == 0)
            {
                ConfigDB.Model.Add(adapter.ToParceiro());
            }
            else
            {
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(f => f.Id == adapter.Id);
                patner.Nome = adapter.Nome;
                patner.Enderec = adapter.Endereco;
                patner.Telefone = adapter.Telefone;
                patner.Site = adapter.Site;
                patner.Facebook = adapter.Facebook;
                patner.Imagem = adapter.Imagem;
                ConfigDB.Model.Add(patner);
            }
            
            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
            
            return RedirectToAction("Parceiros");
        }

        [HttpPost]
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

        [HttpPost]
        public ActionResult EditarParceiro()
        {
            var id = Convert.ToInt32(Request.Form["patner"]);

            var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
            var adapter = new ParceiroAdapter().ToParceiroAdapter(patner);

            return RedirectToAction("Parceiros", new RouteValueDictionary(adapter));
        }
    }
}