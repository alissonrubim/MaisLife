using MaisLife.Controllers;
using MaisLife.Controllers.Admin;
using MaisLifeModel;
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
                controller.ViewBag.Orders = ConfigDB.Model.Pedidos.Where(w => w.Usuario == logged.Id && w.Origem == "Vendedor").OrderBy(w => w.Previsao_entrega).ToList();
                controller.ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(w => w.Id == logged.Id);        
            }
            else
            {
                controller.ViewBag.Orders = ConfigDB.Model.Pedidos.ToList().OrderBy(w => w.Previsao_entrega).ToList();
                controller.ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(w => w.Permissao >= 1).ToList();
            }

            controller.ViewBag.User = logged;
            controller.ViewBag.OutsideClients = ConfigDB.Model.Usuario_externos.ToList();
            controller.ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            controller.ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
                
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

                    ConfigDB.Model.Add(cart);

                    if (ConfigDB.Model.HasChanges)
                        ConfigDB.Model.SaveChanges();

                    cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                }
                home.ViewBag.User = user;
                home.ViewBag.Cart = cart;
               
            }
            else
            {
                home.ViewBag.Cart = Sessions.FindShoppingCart();
            }
            home.ViewBag.Products = ConfigDB.Model.Produtos.ToList();
        }

    }
}