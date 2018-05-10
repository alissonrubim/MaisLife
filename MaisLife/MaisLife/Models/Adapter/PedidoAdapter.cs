using MaisLifeModel;
using MaisLifeModel.Models;
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
        public usuario Usuario { get; set; }
        [Required]
        public endereco Endereco { get; set; }
        [Required]
        public decimal Valor { get; set; }       
        public DateTime Data { get; set; }       
        public string Status { get; set; }
        public carrinho Carrinho { get; set; }
        public decimal Pago { get; set; }
        [Required]
        public string Metodo { get; set; }
        public usuario_externo UsuarioExterno { get; set; }
        [Required]
        public string Origem { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Vencimento { get; set; }
        public string Tipo { get; set; }
        public int? Parcelas { get; set; }
        public string MotivoTroca { get; set; }
        public int Desconto { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PrevisaoEntrega { get; set; }

        public pedido ToPedido(){
             return new pedido()
             {
                id = this.Id,
                usuario1 = this.Usuario,
                endereco1 = this.Endereco,
                valor = this.Valor,
                data = this.Data,
                status = this.Status,
                carrinho1 = this.Carrinho,
                pago = this.Pago,
                metodo = this.Metodo,
                usuario_externo1 = this.UsuarioExterno,
                origem = this.Origem,
                vencimento = this.Vencimento,
                tipo = this.Tipo,
                parcelas = this.Parcelas,
                motivo_troca = this.MotivoTroca,
                desconto = this.Desconto,
                previsao_entrega = this.PrevisaoEntrega
             };
        }

        public PedidoAdapter ToPedidoAdapter(pedido pedido)
        {
            return new PedidoAdapter()
            {
                Id = pedido.id,
                Usuario = pedido.usuario1,
                Endereco = pedido.endereco1,
                Valor = pedido.valor,
                Data = pedido.data,
                Status = pedido.status,
                Carrinho = pedido.carrinho1,
                Pago = pedido.pago.Value,
                Metodo = pedido.metodo,
                UsuarioExterno = pedido.usuario_externo1,
                Origem = pedido.origem,
                Vencimento = pedido.vencimento,
                Tipo = pedido.tipo,
                Parcelas = pedido.parcelas,
                MotivoTroca = pedido.motivo_troca,
                Desconto = pedido.desconto.Value,
                PrevisaoEntrega = (DateTime) pedido.previsao_entrega
            };
        }

        public static string OrderWithOutDiscount(decimal value, int discount) { 
            var a = value + (value * (discount / 100M));
            var b = String.Format("{0:0.00}", a);
            return b;
        }

        public static string OrderDiscountValue(decimal value, int discount)
        {
            var a = (value - (value + (value * (discount / 100M)))) * -1;
            var b = String.Format("{0:0.00}", a);
            return b;
        }

    }

   
}