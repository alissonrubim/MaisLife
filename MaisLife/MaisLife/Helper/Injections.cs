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
            if (logged.Permissao < 2)
            {
                controller.ViewBag.Orders = MaisLifeModel.DatabaseContext.Model.Pedido.Where(w => w.Usuario == logged.Id && w.Origem == "Vendedor").OrderBy(w => w.Previsao_entrega).ToList();
                controller.ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Id == logged.Id);        
            }
            else
            {
                controller.ViewBag.Orders = MaisLifeModel.DatabaseContext.Model.Pedido.Where(w => w.Origem == "Vendedor").OrderBy(w => w.Previsao_entrega).ToList();
                controller.ViewBag.Sellers = MaisLifeModel.DatabaseContext.Model.Usuario.Where(w => w.Permissao >= 1).ToList();
            }

            controller.ViewBag.User = logged;
            controller.ViewBag.OutsideClients = MaisLifeModel.DatabaseContext.Model.Usuario_externo.ToList();
            controller.ViewBag.Products = MaisLifeModel.DatabaseContext.Model.Produto.ToList();
            controller.ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.Bairro.ToList();
                
        }   
        
        public static void LayoutInjection(HomeController home)
        {
            Usuario user = (Usuario)HttpContext.Current.Session["user"];
            if (user != null)
            {
                Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                if (cart == null)
                {
                    cart = new Carrinho()
                    {
                        Usuario1 = user,
                        Status = "Ativo"
                    };

                    MaisLifeModel.DatabaseContext.Model.Carrinho.Add(cart);

                   // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                        MaisLifeModel.DatabaseContext.Model.SaveChanges();

                    cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                }
                home.ViewBag.User = user;
                home.ViewBag.Cart = cart;
               
            }
            else
            {
                home.ViewBag.Cart = Sessions.FindShoppingCart();
            }
            home.ViewBag.Products = MaisLifeModel.DatabaseContext.Model.Produto.ToList();
        }

    }
}