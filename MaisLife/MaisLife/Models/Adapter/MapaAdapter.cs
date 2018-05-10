using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class MapaAdapter{

        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? DataEntrega { get; set; }
        public string Observacao { get; set; }
        public IList<mapa_pedido> MapaPedidos { get; set; }

        public mapaentrega ToMapa() {
            return new mapaentrega()
            {
                id = this.Id,
                data_entrega = this.DataEntrega,
                observacao = this.Observacao,
                mapa_pedido = this.MapaPedidos
            };
        }

        public MapaAdapter ToMapaAdapter(mapaentrega map)
        {
            return new MapaAdapter()
            {
                Id = map.id,
                DataEntrega = map.data_entrega,
                Observacao = map.observacao,
                MapaPedidos = map.mapa_pedido.ToList()
            };
        }

        public decimal CalcTotalAVista() {
            var total = 0.0M;
            foreach (var x in MapaPedidos) {
                if (x.pedido1.metodo == "A vista") {
                    total += x.pedido1.valor;
                }
            }

            return total;
        }

        public decimal CalcTotalAPrazo()
        {
            var total = 0.0M;
            foreach (var x in MapaPedidos)
            {
                if (x.pedido1.metodo == "Prazo")
                {
                    total += x.pedido1.valor;
                }
            }

            return total;
        }

        public decimal CalcTotalAPrazoComTroca()
        {
            var total = 0.0M;
            foreach (var x in MapaPedidos)
            {
                if (x.pedido1.metodo == "Prazo" && x.pedido1.tipo == "Troca")
                {
                    total += x.pedido1.valor;
                }
            }

            return total;
        }

        public decimal CalcTotalAVistaComTroca()
        {
            var total = 0.0M;
            foreach (var x in MapaPedidos)
            {
                if (x.pedido1.metodo == "A vista" && x.pedido1.tipo == "Troca")
                {
                    total += x.pedido1.valor;
                }
            }

            return total;
        }

    }
}