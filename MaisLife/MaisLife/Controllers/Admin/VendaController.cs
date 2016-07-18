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

            ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(w => w.Permissao >= 1).ToList();
            ViewBag.Clients = ConfigDB.Model.Usuarios.Where(w => w.Permissao == 0 && w.Id > 1).ToList();
            ViewBag.OutsideClients = ConfigDB.Model.Usuario_externos.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
            return View();
        }

        public ActionResult FilterOrders()
        {
            var viewmodel = new VendaViewModel(Request);
            ViewBag.Orders = viewmodel.DoSearch(1);

            ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(w => w.Permissao >= 1).ToList();
            ViewBag.Clients = ConfigDB.Model.Usuarios.Where(w => w.Permissao == 0 && w.Id > 1).ToList();
            ViewBag.OutsideClients = ConfigDB.Model.Usuario_externos.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
            
            return View("Index");
        }

        public ActionResult ProfileView(int id) {
            var order = ConfigDB.Model.Pedidos.FirstOrDefault(w => w.Id == id);
            return View(order);
        }
    }
}