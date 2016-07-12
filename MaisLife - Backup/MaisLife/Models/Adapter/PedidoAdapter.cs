using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class PedidoAdapter{

        public int Id { get; set; }
        [Required]
        public Usuario Usuario { get; set; }
        [Required]
        public Endereco Endereco { get; set; }
        [Required]
        public decimal Valor { get; set; }       
        public DateTime Data { get; set; }       
        public string Status { get; set; }
        public Carrinho Carrinho { get; set; }
        public decimal Pago { get; set; }
        [Required]
        public string Metodo { get; set; }
        public Usuario_externo UsuarioExterno { get; set; }
        [Required]
        public string Origem { get; set; }

        public Pedido ToPedido(){
             return new Pedido()
             {
                Id = this.Id,
                Usuario1 = this.Usuario,
                Endereco1 = this.Endereco,
                Valor = this.Valor,
                Data = this.Data,
                Status = this.Status,
                Carrinho1 = this.Carrinho,
                Pago = this.Pago,
                Metodo = this.Metodo,
                Usuario_externo1 = this.UsuarioExterno,
                Origem = this.Origem
             };
        }

        public PedidoAdapter ToPedidoAdapter(Pedido pedido)
        {
            return new PedidoAdapter()
            {
                Id = pedido.Id,
                Usuario = pedido.Usuario1,
                Endereco = pedido.Endereco1,
                Valor = pedido.Valor,
                Data = pedido.Data,
                Status = pedido.Status,
                Carrinho = pedido.Carrinho1,
                Pago = pedido.Pago,
                Metodo = pedido.Metodo,
                UsuarioExterno = pedido.Usuario_externo1,
                Origem = pedido.Origem
            };
        }

    }

   
}