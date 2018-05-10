using MaisLife.Helper;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.ViewModels.AdminViewModels
{
    public class ProdutoViewModel{

        public HttpRequestBase Request { get; set; }
        public Produto Produto { get; set; }

        public ProdutoViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public ProdutoViewModel(HttpRequestBase r, ProdutoAdapter adapter)
        {
            this.Request = r;
            this.Produto = adapter.ToProduto();
        }

        public void ProdutoCreateOrEdit() {           
            
            if (this.Produto.Id > 0)
            {  

                var newProduct = this.Produto;
                this.Produto = MaisLifeModel.DatabaseContext.Model.Produto.FirstOrDefault(f => f.Id == this.Produto.Id);
                
                foreach (var rel in this.Produto.Produto_bairros)
                {
                    MaisLifeModel.DatabaseContext.Model.Produto_bairro.Remove(rel);
                }

                this.Produto.Nome = newProduct.Nome;
                this.Produto.Descricao = newProduct.Descricao;
                this.Produto.Preco = newProduct.Preco;
                this.Produto.Unidade = newProduct.Unidade;
                this.Produto.Imagem = newProduct.Imagem;
                this.Produto.Dias_entrega = newProduct.Dias_entrega;
            }

            var fr = new FastRequest(this.Request);
            
            var amount = fr.ToInt("delivery-amount");
            if (amount > 0)
            {
                this.Produto.Produto_bairros = new List<Produto_bairro>();
                for (var i = 1; i <= amount; i++)
                {
                    var local = fr.ToInt("delivery-local-" + i);
                    var tax = fr.ToDecimal("delivery-tax-" + i);

                    var rel = new Produto_bairro()
                    {
                        Bairro1 = MaisLifeModel.DatabaseContext.Model.Bairro.FirstOrDefault(b => b.Id == local),
                        Produto1 = this.Produto,
                        Taxa = tax
                    };

                    this.Produto.Produto_bairros.Add(rel);

                }
            }      

            MaisLifeModel.DatabaseContext.Model.Produto.Add(this.Produto);

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public void DoRemove() {
            var fr = new FastRequest(this.Request);
            
            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var product = MaisLifeModel.DatabaseContext.Model.Produto.FirstOrDefault(p => p.Id == id);
                MaisLifeModel.DatabaseContext.Model.Produto.Remove(product);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public int DoEdit() {
            var fr = new FastRequest(this.Request);            
            return fr.ToInt("item");
        }

    }
}