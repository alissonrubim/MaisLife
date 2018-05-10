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
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index(int id = 0)
        {
            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.bairro.ToList();
            ViewBag.Products = MaisLifeModel.DatabaseContext.Model.produto.ToList();

            if (id > 0)
            {
                var produto = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(f => f.id == id);
                var adapter = new ProdutoAdapter().ToProdutoAdapter(produto);
                return View(adapter);
            }else{
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult CreateOrEditProduto(ProdutoAdapter adapter, HttpPostedFileBase file = null)
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

            var viewmodel = new ProdutoViewModel(Request, adapter);
            viewmodel.ProdutoCreateOrEdit();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult RemoveItem()
        {
            var viewmodel = new ProdutoViewModel(Request);
            viewmodel.DoRemove();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult SendItemToEdit()
        {
            var viewmodel = new ProdutoViewModel(Request);  
            return RedirectToAction("Index", new { id = viewmodel.DoEdit() });
        }
    }
}