using MaisLife.Helper;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.ViewModels
{
    public class VendaExternaViewModel{

        public HttpRequestBase Request { get; set; }
        public Pedido Order { get; set; }
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
                if (this.Order.Usuario_externo1.Id > 0)
                    this.Order.Usuario_externo1 = ConfigDB.Model.Usuario_externos.FirstOrDefault(f => f.Id == this.Order.Usuario_externo1.Id);

                if (this.Order.Id > 0)
                {
                    // EDITAR                  
                    var newOrder = this.Order;
                    this.Order = ConfigDB.Model.Pedidos.FirstOrDefault(f => f.Id == Order.Id);
                    this.Order.Usuario1 = ConfigDB.Model.Usuarios.FirstOrDefault(f => f.Id == newOrder.Usuario1.Id);
                    this.Order.Carrinho1 = ConfigureCart(this.Order.Carrinho1);
                    this.Order.Endereco1 = newOrder.Usuario_externo1.Endereco1;
                    this.Order.Status = newOrder.Status;
                    this.Order.Metodo = newOrder.Metodo;                   
                    this.Order.Tipo = newOrder.Tipo;
                    this.Order.Desconto = newOrder.Desconto;
                    
                    if (this.Order.Metodo != "Boleto")
                        this.Order.Vencimento = null;
                    else
                        this.Order.Vencimento = newOrder.Vencimento;

                    if (this.Order.Metodo != "Prazo")
                        this.Order.Parcelas = null;
                    else
                        this.Order.Parcelas = newOrder.Parcelas;

                    if (this.Order.Tipo != "Troca")
                        this.Order.Motivo_troca = null;
                    else
                        this.Order.Motivo_troca = newOrder.Motivo_troca; 

                }
                else
                {     
                    // NOVO 
                    this.Order.Usuario1 = ConfigDB.Model.Usuarios.FirstOrDefault(f => f.Id == this.Order.Usuario1.Id);
                    this.Order.Carrinho1 = ConfigureCart();
                    this.Order.Endereco1 = this.Order.Usuario_externo1.Endereco1;
                    this.Order.Status = OnCreateStatus;
                    this.Order.Data = DateTime.Now;                   
                }

                // VALOR DO PEDIDO
                this.Order.Valor = 0;
                // COLOCAR ITENS NO CARRINHO
                for (var i = 1; i <= productCount; i++)
                {
                    var productId = fr.ToInt("product-" + i);
                    var productAmount = fr.ToInt("product-count-" + i);

                    var product = ConfigDB.Model.Produtos.FirstOrDefault(f => f.Id == productId);                   

                    this.Order.Carrinho1.Carrinho_produtos.Add(new Carrinho_produto() {
                        Produto1 = product,
                        Quantidade = productAmount,
                        Carrinho1 = this.Order.Carrinho1
                    });

                    this.Order.Valor += (decimal)product.Preco * productAmount;
                }

                this.Order.Origem = Source;
                this.Order.Previsao_entrega = findShippingDate();
                
                ConfigDB.Model.Add(this.Order);
                if (ConfigDB.Model.HasChanges)
                    ConfigDB.Model.SaveChanges();

            }
        }

        private DateTime? findShippingDate()
        {
            int maxPrize = 0;
            foreach (var x in this.Order.Carrinho1.Carrinho_produtos) {
                if (x.Produto1.Dias_entrega > maxPrize)
                    maxPrize = x.Produto1.Dias_entrega;
            }

            var today = DateTime.Now;
            return today.AddDays(maxPrize);

        }
        
        public Carrinho ConfigureCart(Carrinho cart = null)
        {
            if (cart == null)
            {
                return new Carrinho()
                {
                    Carrinho_produtos = new List<Carrinho_produto>(),
                    Status = "Fechado",
                    Usuario1 = this.Order.Usuario1
                };
            }
            else
            {  
                ConfigDB.Model.Delete(cart.Carrinho_produtos);
                cart.Carrinho_produtos = new List<Carrinho_produto>();
                cart.Usuario1 = this.Order.Usuario1;
                return cart;
            }
        }

        public int DoEdit() {
            var fr = new FastRequest(this.Request);
          
            var user = Helper.App.Logged();           
            var orderId = fr.ToInt("item");
            var order = ConfigDB.Model.Pedidos.FirstOrDefault(f => f.Id == orderId);
            
            if (user.Permissao < 2)
            {
                if (order.Status == "Em aberto")
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
                var order = ConfigDB.Model.Pedidos.FirstOrDefault(p => p.Id == id);
                ConfigDB.Model.Delete(order);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
        }

        internal PedidoAdapter GetAdapterFromPedidoId(int id)
        {
            var order = ConfigDB.Model.Pedidos.FirstOrDefault(f => f.Id == id);
            return new PedidoAdapter().ToPedidoAdapter(order);
        }
    }
}