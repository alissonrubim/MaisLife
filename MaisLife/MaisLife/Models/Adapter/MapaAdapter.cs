using MaisLifeModel;
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
        public DateTime? DataEntrega { get; set; }
        public string Observacao { get; set; }
        private IList<Mapa_pedido> MapaPedidos { get; set; }

        public Mapaentrega ToMapa() {
            return new Mapaentrega()
            {
                Id = this.Id,
                Data_entrega = this.DataEntrega,
                Observacao = this.Observacao,
                Mapa_pedidos = this.MapaPedidos
            };
        }

        public MapaAdapter ToMapaAdapter(Mapaentrega map)
        {
            return new MapaAdapter()
            {
                Id = map.Id,
                DataEntrega = map.Data_entrega,
                Observacao = map.Observacao,
                MapaPedidos = map.Mapa_pedidos
            };
        }

    }
}