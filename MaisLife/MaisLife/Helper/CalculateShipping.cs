using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public static class CalculateShipping
    {
        
        public static decimal Calculate(Carrinho cart, Bairro local)
        {
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

    }
}