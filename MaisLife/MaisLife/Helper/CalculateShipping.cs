using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public static class CalculateShipping
    {
        
        public static decimal Calculate(MaisLifeModel.Models.bairro local)
        {

            var cart = getCartActive();
            var prices = new List<decimal>();
            foreach(MaisLifeModel.Models.carrinho_produto cp in cart.carrinho_produto){


                var value = MaisLifeModel.DatabaseContext.Model.produto_bairro.FirstOrDefault(f => f.bairro == local.id && f.produto == cp.produto);
                if (value != null)
                {
                    prices.Add(value.taxa);
                }
                else
                {
                    prices.Add(local.taxa.Value);
                }
            }
            return prices.Min();
        }

        public static carrinho getCartActive(){
            carrinho cart;
            usuario user = (usuario)HttpContext.Current.Session["user"];
            if (user == null)
            {
               return cart = Sessions.FindShoppingCart();
            }
            else
            {
              return  cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
            }
        }

        public static DateTime? findShippingDate(MaisLifeModel.Models.pedido order)
        {
            int maxPrize = 0;
            foreach (var x in order.carrinho1.carrinho_produto)
            {
                if (x.produto1.dias_entrega > maxPrize)
                    maxPrize = x.produto1.dias_entrega;
            }

            var today = DateTime.Now;
            return today.AddDays(maxPrize);

        }

    }
}