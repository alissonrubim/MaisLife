using MaisLife.Models.Adapter;
using MaisLife.ViewModels;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MaisLife.Controllers.Admin
{
    public class VendaExternaController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            var viewmodel = new VendaExternaViewModel(Request);
            Helper.Injections.VendaExternaIndexInjection(this);

            if (id > 0)
            {
                var adapter = viewmodel.GetAdapterFromPedidoId(id);
                ViewBag.Cart = adapter.Carrinho;
                return View(adapter);
            }
            else {
                return View();
            }
       
        }

        [HttpPost]
        public ActionResult CreateOrEditVendaExterna(PedidoAdapter adapter)
        {
            var viewmodel = new VendaExternaViewModel(Request, adapter);
            viewmodel.VendaExternaCreateOrEdit();

            return RedirectToAction("index", new { id = 0 });

        }

        [HttpPost]
        public ActionResult SendItemToEdit()
        {
            var viewmodel = new VendaExternaViewModel(Request);
            return RedirectToAction("Index", new { id = viewmodel.DoEdit() });

        }

        [HttpPost]
        public ActionResult RemoveItem()
        {
            var viewmodel = new VendaExternaViewModel(Request);
            viewmodel.DoRemove();
            return RedirectToAction("Index");
           
        }

        public string AjaxUse_ClientsQuery()
        {
            var id = Convert.ToInt32(Request.Form["exid"]);
            var externalUser = MaisLifeModel.DatabaseContext.Model.usuario_externo.FirstOrDefault(eu => eu.id == id);

            // ANULAR REFERÊNCIAS
            externalUser.endereco1.bairro1.endereco = null;
            externalUser.endereco1.usuario_externo = null;
            externalUser.endereco1.usuario1 = null;
            externalUser.pedido = null;
            externalUser.endereco1.pedido = null;

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(externalUser);
        }

        public string AjaxUse_ProductsQuery()
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            var product = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(p => p.id == id);

            // ANULAR REFERÊNCIAS
            product.produto_bairro = null;
            product.carrinho_produto = null;

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(product);
        }
        
    }
}