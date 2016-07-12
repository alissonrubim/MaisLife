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
    public class MapaEntregaController : Controller
    {
        // GET: MapaEntrega
        public ActionResult Index()
        {
            ViewBag.Maps = ConfigDB.Model.Mapaentregas.ToList();
            var orders = ConfigDB.Model.Pedidos.Where(w => w.Status == "Em aberto").OrderBy(w => w.Previsao_entrega).ToList();
            var adapters = new List<PedidoAdapter>();
            foreach (var order in orders) {
                var adapter = new PedidoAdapter().ToPedidoAdapter(order);
                adapters.Add(adapter);
            }
            ViewBag.Orders = adapters;
            return View();
        }

        public ActionResult CreateOrEditMap(MapaAdapter adapter) {

            var viewmodel = new MapaEntregaViewModel(Request, adapter);
            viewmodel.CreateOrEditMap();
            
            return RedirectToAction("Index", new { id = 0 });

        }
    }
}