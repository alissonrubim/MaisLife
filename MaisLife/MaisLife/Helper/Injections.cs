using MaisLife.Controllers;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class Injections{

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