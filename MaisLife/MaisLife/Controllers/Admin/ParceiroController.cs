using MaisLife.Models.Adapter;
using MaisLife.ViewModels.AdminViewModels;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers.Admin
{
    public class ParceiroController : Controller
    {
        // GET: Parceiro
        public ActionResult Index(int id = 0)
        {  
            ViewBag.Patners = MaisLifeModel.DatabaseContext.Model.Parceiro.ToList();
            
            if (id > 0) {
                var patner = MaisLifeModel.DatabaseContext.Model.Parceiro.FirstOrDefault(p => p.Id == id);
                var adapter = new ParceiroAdapter().ToParceiroAdapter(patner);
                return View(adapter);
            }else{
                return View();
            }              
       
        }

        [HttpPost]
        public ActionResult CreateOrEditParceiro(ParceiroAdapter adapter, HttpPostedFileBase file = null)
        {

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Uploads"), file.FileName);
                    file.SaveAs(path);
                    adapter.Imagem = file.FileName;
                }
            }

            var viewmodel = new ParceiroViewModel(Request, adapter);
            viewmodel.ParceiroCreateOrEdit();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult RemoveItem()
        {
            var viewmodel = new ParceiroViewModel(Request);
            viewmodel.DoRemove();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult SendItemToEdit()
        {
            var viewmodel = new ParceiroViewModel(Request);            

            return RedirectToAction("Index", new { id = viewmodel.DoEdit() });
        }
    }
}