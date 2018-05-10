using MaisLife.Helper;
using MaisLifeModel;
using MaisLifeModel.Models;
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
        public string Preco { get; set; }
        [Required]
        public int Unidade { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Imagem { get; set; }
        public IList<produto_bairro> Bairros { 
            get {
                return MaisLifeModel.DatabaseContext.Model.produto_bairro.Where(pb => pb.produto == Id).ToList();
            }  
        }
        [Required]
        public int DiasEntrega { get; set; }

        public produto ToProduto()
        {
            return new produto()
            {
                id = this.Id,
                nome = this.Nome,
                descricao = this.Descricao,
                preco = Converter.ConvertMoney(this.Preco),
                unidade = this.Unidade,
                imagem = this.Imagem,
                produto_bairro = this.Bairros,
                dias_entrega = this.DiasEntrega
            };
        }

        public ProdutoAdapter ToProdutoAdapter(produto produto)
        {
            var adapter = new ProdutoAdapter()
            {
                Id = produto.id,
                Nome = produto.nome,
                Descricao = produto.descricao,
                Preco = Convert.ToString(produto.preco),
                Unidade = (int)produto.unidade,
                Imagem = produto.imagem,
                DiasEntrega = produto.dias_entrega
            };            

            return adapter;
        }

    }
}