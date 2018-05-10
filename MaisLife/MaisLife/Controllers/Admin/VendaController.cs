using MaisLife.ViewModels.AdminViewModels;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers.Admin
{
    public class VendaController : Controller
    {
        // GET: Venda
        public ActionResult Index()
        {
            var viewmodel = new VendaViewModel(Request);
            ViewBag.Orders = viewmodel.DoSearch(0);

            ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Permissao >= 1).ToList();
            ViewBag.Clients = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Permissao == 0 && w.Id > 1).ToList();
            ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.Usuario_externo.ToList();
            ViewBag.Products = MaisLifeModel.DatabaseContext.Model.Produto.ToList();
            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.Bairro.ToList();
            return View();
        }

        public ActionResult FilterOrders()
        {
            var viewmodel = new VendaViewModel(Request);
            ViewBag.Orders = viewmodel.DoSearch(1);

            ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Permissao >= 1).ToList();
            ViewBag.Clients = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Permissao == 0 && w.Id > 1).ToList();
            ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.Usuario_externo.ToList();
            ViewBag.Products = MaisLifeModel.DatabaseContext.Model.Produto.ToList();
            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.Bairro.ToList();
            
            return View("Index");
        }

        public ActionResult ProfileView(int id) {
            var order = MaisLifeModel.DatabaseContext.Model.Pedido.FirstOrDefault(w => w.Id == id);
            return View(order);
        }
    }
}