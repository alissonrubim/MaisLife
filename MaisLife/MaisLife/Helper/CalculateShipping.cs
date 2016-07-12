using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public static class CalculateShipping
    {
        
        public static decimal Calculate(Bairro local)
        {

            var cart = getCartActive();
            var prices = new List<decimal>();
            foreach(Carrinho_produto cp in cart.Carrinho_produtos){
                var value = ConfigDB.Model.Produto_bairros.FirstOrDefault
                    (f => f.Bairro == local.Id && f.Produto == cp.Produto);
                if (value != null)
                {
                    prices.Add(value.Taxa);
                }
                else
                {
                    prices.Add(local.Taxa);
                }
            }
            return prices.Min();
        }

        public static Carrinho getCartActive(){
            Carrinho cart;
            Usuario user = (Usuario)HttpContext.Current.Session["user"];
            if (user == null)
            {
               return cart = Sessions.FindShoppingCart();
            }
            else
            {
              return  cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
            }
        }

    }
}