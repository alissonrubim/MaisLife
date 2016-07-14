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
        [Required]
        public int Unidade { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Imagem { get; set; }
        public IList<Produto_bairro> Bairros { 
            get {
                return ConfigDB.Model.Produto_bairros.Where(pb => pb.Produto == Id).ToList();
            }  
        }
        [Required]
        public int DiasEntrega { get; set; }

        public Produto ToProduto()
        {
            return new Produto()
            {
                Id = this.Id,
                Nome = this.Nome,
                Descricao = this.Descricao,
                Preco = this.Preco,
                Unidade = this.Unidade,
                Imagem = this.Imagem,
                Produto_bairros = this.Bairros,
                Dias_entrega = this.DiasEntrega
            };
        }

        public ProdutoAdapter ToProdutoAdapter(Produto produto)
        {
            var adapter = new ProdutoAdapter()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = (Decimal)produto.Preco,
                Unidade = (int)produto.Unidade,
                Imagem = produto.Imagem,
                DiasEntrega = DiasEntrega
            };            

            return adapter;
        }

    }
}