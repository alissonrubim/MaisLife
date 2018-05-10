using MaisLife.Models.Adapter;
using MaisLife.ViewModels.AdminViewModels;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers.Admin
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index(int id = 0)
        {
            ViewBag.Users = MaisLifeModel.DatabaseContext.Model.Usuario.ToList();
            if (id > 0)
            {
                var user = MaisLifeModel.DatabaseContext.Model.Usuario.FirstOrDefault(f => f.Id == id);
                var adapter = new UsuarioAdapter().ToUsuarioAdapter(user);
                return View(adapter);
            }
            else { 
                return View();
            }
            
            
        }

        public ActionResult CreateOrEditUsuario(UsuarioAdapter adapter) {
            var viewModel = new UsuarioViewModel(Request, adapter);
            viewModel.UsuarioCreateOrEdit();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult RemoveItem()
        {
            var viewmodel = new UsuarioViewModel(Request);
            viewmodel.DoRemove();

            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult SendItemToEdit()
        {
            var viewmodel = new UsuarioViewModel(Request);
            return RedirectToAction("Index", new { id = viewmodel.DoEdit() });
        }
    }

    
}