using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisLifeModel.Models
{
    public partial class carrinho
    {
        public carrinho_produto checkProduct(produto produto)
        {
            foreach (var rel in carrinho_produto)
            {
                if (rel.produto == produto.id)
                    return rel;
            }

            return null;
        }

        public decimal Total(decimal sum)
        {
            decimal? total = 0;
            foreach (var rel in carrinho_produto)
            {
                total += rel.quantidade * rel.produto1.preco;
            }

            return (decimal)total + sum;

        }


        public int TotalItens()
        {
            int amount = 0;
            foreach (var rel in carrinho_produto)
            {
                amount += (int)rel.quantidade;
            }

            return amount;
        }

    }
}
