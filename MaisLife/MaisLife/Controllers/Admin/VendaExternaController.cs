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
            var externalUser = ConfigDB.Model.Usuario_externos.FirstOrDefault(eu => eu.Idusuario == id);

            // ANULAR REFERÊNCIAS
            externalUser.Endereco1.Bairro1.Enderecos = null;
            externalUser.Endereco1.Usuario_externos = null;
            externalUser.Endereco1.Usuario1 = null;
            externalUser.Pedidos = null;
            externalUser.Endereco1.Pedidos = null;

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(externalUser);
        }

        public string AjaxUse_ProductsQuery()
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            var product = ConfigDB.Model.Produtos.FirstOrDefault(p => p.Id == id);

            // ANULAR REFERÊNCIAS
            product.Produto_bairros = null;
            product.Carrinho_produtos = null;

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(product);
        }
        
    }
}