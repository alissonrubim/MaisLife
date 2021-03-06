﻿using MaisLife.Models.Adapter;
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
        public ActionResult Index(int id = 0)
        {
            ViewBag.Maps = MaisLifeModel.DatabaseContext.Model.mapaentrega.ToList();
            var orders = MaisLifeModel.DatabaseContext.Model.pedido.Where(w => w.status == "Em aberto").OrderBy(w => w.previsao_entrega).ToList();
            var adapters = new List<PedidoAdapter>();
            foreach (var order in orders) {
                var adapter = new PedidoAdapter().ToPedidoAdapter(order);
                adapters.Add(adapter);
            }
            ViewBag.Orders = adapters;
            if (id > 0)
            {
                var map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(f => f.id == id);
                var adapter = new MapaAdapter().ToMapaAdapter(map);
                return View(adapter);
            }
            else{
                return View();
            }
                
        }

        public ActionResult CreateOrEditMap(MapaAdapter adapter) {

            var viewmodel = new MapaEntregaViewModel(Request, adapter);
            viewmodel.CreateOrEditMap();
            
            return RedirectToAction("Index", new { id = 0 });

        }

        [HttpPost]
        public ActionResult SendItemToEdit() {
            var viewmodel = new MapaEntregaViewModel(Request);

            return RedirectToAction("Index", new { id = viewmodel.DoEdit() });
        }

        [HttpPost]
        public ActionResult SearchMap() {
            var viewmodel = new MapaEntregaViewModel(Request);
            var id = viewmodel.DoSearch();
            if (id == 0)
            {
                ViewBag.Erro = "fds";
            }           
            return RedirectToAction("Index", new { id = id });
            
        }

        [HttpPost]
        public ActionResult ConfirmMap() {
            var viewmodel = new MapaEntregaViewModel(Request);
            viewmodel.DoConfirm();
            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpPost]
        public ActionResult RemoveItem()
        {
            var viewmodel = new MapaEntregaViewModel(Request);
            viewmodel.DoRemove();

            return RedirectToAction("Index", new { id = 0 });
        }

        public ActionResult PrintMap(int id) {
            var viewmodel = new MapaEntregaViewModel(Request);
            return View(viewmodel.DoPrint(id));
        }
    }
}