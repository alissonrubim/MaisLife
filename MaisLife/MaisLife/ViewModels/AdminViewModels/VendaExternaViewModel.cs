using MaisLife.Helper;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.ViewModels
{
    public class VendaExternaViewModel{

        public HttpRequestBase Request { get; set; }
        public pedido Order { get; set; }
        public const string Source = "Vendedor";
        public const string OnCreateStatus = "Em aberto";

        public VendaExternaViewModel(HttpRequestBase r) {
            this.Request = r;
        }
        
        public VendaExternaViewModel(HttpRequestBase r, PedidoAdapter adapter)
        {
            this.Request = r;
            this.Order = adapter.ToPedido();
        }
        
        public void VendaExternaCreateOrEdit()
        {
            var fr = new FastRequest(this.Request);

            var productCount = fr.ToInt("product-amount");
            if (productCount > 0)
            {                
                // CHECA SE UM CLIENTE PRONTO FOI SELECIONADO
                if (this.Order.usuario_externo1.id > 0)
                    this.Order.usuario_externo1 = MaisLifeModel.DatabaseContext.Model.usuario_externo.FirstOrDefault(f => f.id == this.Order.usuario_externo1.id);

                if (this.Order.id > 0)
                {
                    // EDITAR                  
                    var newOrder = this.Order;
                    this.Order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(f => f.id == Order.id);
                    this.Order.usuario1 = MaisLifeModel.DatabaseContext.Model.usuario.FirstOrDefault(f => f.id == newOrder.usuario1.id);
                    this.Order.carrinho1 = ConfigureCart(this.Order.carrinho1);
                    this.Order.endereco1 = newOrder.usuario_externo1.endereco1;
                    this.Order.status = newOrder.status;
                    this.Order.metodo = newOrder.metodo;                   
                    this.Order.tipo = newOrder.tipo;
                    this.Order.desconto = newOrder.desconto;
                    
                    if (this.Order.metodo != "Boleto")
                        this.Order.vencimento = null;
                    else
                        this.Order.vencimento = newOrder.vencimento;

                    if (this.Order.metodo != "Prazo")
                        this.Order.parcelas = null;
                    else
                        this.Order.parcelas = newOrder.parcelas;

                    if (this.Order.tipo != "Troca")
                        this.Order.motivo_troca = null;
                    else
                        this.Order.motivo_troca = newOrder.motivo_troca; 

                }
                else
                {     
                    // NOVO 
                    this.Order.usuario1 = MaisLifeModel.DatabaseContext.Model.usuario.FirstOrDefault(f => f.id == this.Order.usuario1.id);
                    this.Order.carrinho1 = ConfigureCart();
                    this.Order.endereco1 = this.Order.usuario_externo1.endereco1;
                    this.Order.status = OnCreateStatus;
                    this.Order.data = DateTime.Now;                   
                }

                // VALOR DO PEDIDO
                this.Order.valor = 0;
                // COLOCAR ITENS NO CARRINHO
                for (var i = 1; i <= productCount; i++)
                {
                    var productId = fr.ToInt("product-" + i);
                    var productAmount = fr.ToInt("product-count-" + i);

                    var product = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(f => f.id == productId);                   

                    this.Order.carrinho1.carrinho_produto.Add(new carrinho_produto() {
                        produto1 = product,
                        quantidade = productAmount,
                        carrinho1 = this.Order.carrinho1
                    });

                    this.Order.valor += (decimal)product.preco * productAmount;
                }

                this.Order.origem = Source;
                this.Order.previsao_entrega = Helper.CalculateShipping.findShippingDate(this.Order);
                var percent = this.Order.desconto / 100M;
                var discount = (this.Order.valor * percent);
                this.Order.valor -= discount.Value; 
                
                MaisLifeModel.DatabaseContext.Model.pedido.Add(this.Order);
                //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                    MaisLifeModel.DatabaseContext.Model.SaveChanges();

            }
        }
        
        public MaisLifeModel.Models.carrinho ConfigureCart(MaisLifeModel.Models.carrinho cart = null)
        {
            if (cart == null)
            {
                return new carrinho()
                {
                    carrinho_produto = new List<carrinho_produto>(),
                    status = "Fechado",
                    usuario1 = this.Order.usuario1
                };
            }
            else
            {  
                foreach(carrinho_produto cp in cart.carrinho_produto)
                    MaisLifeModel.DatabaseContext.Model.carrinho_produto.Remove(cp);
                cart.carrinho_produto = new List<carrinho_produto>();
                cart.usuario1 = this.Order.usuario1;
                return cart;
            }
        }

        public int DoEdit() {
            var fr = new FastRequest(this.Request);
          
            var user = Helper.App.Logged();           
            var orderId = fr.ToInt("item");
            var order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(f => f.id == orderId);
            
            if (user.permissao < 2)
            {
                if (order.status == "Em aberto")
                {
                    return orderId;
                }
                else
                    return 0;
            }
            else
            {
                return orderId;
            }
        }


        internal void DoRemove()
        {
            var fr = new FastRequest(this.Request);

            var countItensToRemove = fr.ToInt("count");
            for (var i = 1; i <= countItensToRemove; i++)
            {
                var id = Convert.ToInt32(this.Request.Form["item-" + i]);
                var order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(p => p.id == id);
                MaisLifeModel.DatabaseContext.Model.pedido.Remove(order);
            }

           // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        internal PedidoAdapter GetAdapterFromPedidoId(int id)
        {
            var order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(f => f.id == id);
            return new PedidoAdapter().ToPedidoAdapter(order);
        }
    }
}