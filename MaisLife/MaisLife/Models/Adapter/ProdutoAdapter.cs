using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class ProdutoAdapter{

        public int Id { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        public string Nome { get; set; }
        [Required]        
        public string Descricao { get; set; }
        [Required]       
        public decimal Preco { get; set; }

        public Produto ToProduto()
        {
            return new Produto()
            {
                Nome = this.Nome,
                Descricao = this.Descricao,
                Preco = this.Preco
            };
        }

        public ProdutoAdapter ToProdutoAdapter(Produto produto)
        {
            return new ProdutoAdapter()
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = (Decimal) produto.Preco
            };
        }

    }
}