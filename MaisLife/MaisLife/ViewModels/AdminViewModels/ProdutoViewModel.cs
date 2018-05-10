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
        public produto Produto { get; set; }

        public ProdutoViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public ProdutoViewModel(HttpRequestBase r, ProdutoAdapter adapter)
        {
            this.Request = r;
            this.Produto = adapter.ToProduto();
        }

        public void ProdutoCreateOrEdit() {           
            
            if (this.Produto.id > 0)
            {  

                var newProduct = this.Produto;
                this.Produto = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(f => f.id == this.Produto.id);
                
                foreach (var rel in this.Produto.produto_bairro)
                {
                    MaisLifeModel.DatabaseContext.Model.produto_bairro.Remove(rel);
                }

                this.Produto.nome = newProduct.nome;
                this.Produto.descricao = newProduct.descricao;
                this.Produto.preco = newProduct.preco;
                this.Produto.unidade = newProduct.unidade;
                this.Produto.imagem = newProduct.imagem;
                this.Produto.dias_entrega = newProduct.dias_entrega;
            }

            var fr = new FastRequest(this.Request);
            
            var amount = fr.ToInt("delivery-amount");
            if (amount > 0)
            {
                this.Produto.produto_bairro = new List<produto_bairro>();
                for (var i = 1; i <= amount; i++)
                {
                    var local = fr.ToInt("delivery-local-" + i);
                    var tax = fr.ToDecimal("delivery-tax-" + i);

                    var rel = new produto_bairro()
                    {
                        bairro1 = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(b => b.id == local),
                        produto1 = this.Produto,
                        taxa = tax
                    };

                    this.Produto.produto_bairro.Add(rel);

                }
            }      

            MaisLifeModel.DatabaseContext.Model.produto.Add(this.Produto);

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public void DoRemove() {
            var fr = new FastRequest(this.Request);
            
            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var product = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(p => p.id == id);
                MaisLifeModel.DatabaseContext.Model.produto.Remove(product);
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