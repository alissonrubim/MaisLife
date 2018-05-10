using MaisLife.Controllers;
using MaisLife.Controllers.Admin;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class Injections{

        public static void VendaExternaIndexInjection(VendaExternaController controller) {
            var logged = Helper.App.Logged();
            if (logged.permissao < 2)
            {
                controller.ViewBag.Orders = MaisLifeModel.DatabaseContext.Model.pedido.Where(w => w.usuario == logged.id && w.origem == "Vendedor").OrderBy(w => w.previsao_entrega).ToList();
                controller.ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.id == logged.id);        
            }
            else
            {
                controller.ViewBag.Orders = MaisLifeModel.DatabaseContext.Model.pedido.Where(w => w.origem == "Vendedor").OrderBy(w => w.previsao_entrega).ToList();
                controller.ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.usuario.Where(w => w.permissao >= 1).ToList();
            }

            controller.ViewBag.User = logged;
            controller.ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.usuario_externo.ToList();
            controller.ViewBag.Products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
            controller.ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.bairro.ToList();
                
        }   
        
        public static void LayoutInjection(HomeController home)
        {
            usuario user = (usuario)HttpContext.Current.Session["user"];
            if (user != null)
            {
                carrinho cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                if (cart == null)
                {
                    cart = new carrinho()
                    {
                        usuario1 = user,
                        status = "Ativo"
                    };

                    MaisLifeModel.DatabaseContext.Model.carrinho.Add(cart);

                   // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                        MaisLifeModel.DatabaseContext.Model.SaveChanges();

                    cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                }
                home.ViewBag.User = user;
                home.ViewBag.Cart = cart;
               
            }
            else
            {
                home.ViewBag.Cart = Sessions.FindShoppingCart();
            }
            home.ViewBag.Products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
        }

    }
}