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

            ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.permissao >= 1).ToList();
            ViewBag.Clients = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.permissao == 0 && w.id > 1).ToList();
            ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.usuario_externo.ToList();
            ViewBag.Products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.bairro.ToList();
            return View();
        }

        public ActionResult FilterOrders()
        {
            var viewmodel = new VendaViewModel(Request);
            ViewBag.Orders = viewmodel.DoSearch(1);

            ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.permissao >= 1).ToList();
            ViewBag.Clients = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.permissao == 0 && w.id > 1).ToList();
            ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.usuario_externo.ToList();
            ViewBag.Products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.bairro.ToList();
            
            return View("Index");
        }

        public ActionResult ProfileView(int id) {
            var order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(w => w.id == id);
            return View(order);
        }
    }
}