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
    public class MapaEntregaViewModel
    {
        public HttpRequestBase Request { get; set; }
        public mapaentrega Map { get; set; }

        public MapaEntregaViewModel(HttpRequestBase Request)
        {            
            this.Request = Request;
        }

        public MapaEntregaViewModel(HttpRequestBase Request, Models.Adapter.MapaAdapter adapter)
        {           
            this.Request = Request;
            this.Map = adapter.ToMapa();
        }

        public void CreateOrEditMap()
        {
            var fr = new FastRequest(this.Request);

            if (this.Map.id > 0) { 
                var newMap = this.Map;
                this.Map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(f => f.id == this.Map.id);
                this.Map.observacao = newMap.observacao;
                this.Map.data_entrega = newMap.data_entrega;

                foreach (var x in this.Map.mapa_pedido) {
                    x.pedido1.status = "Em aberto";
                }
                
                foreach(mapa_pedido mp in this.Map.mapa_pedido)
                    MaisLifeModel.DatabaseContext.Model.mapa_pedido.Remove(mp);
            }
            
            this.Map.mapa_pedido = new List<mapa_pedido>();

            var orderCount = fr.ToInt("orderCount");
            for (var i = 1; i <= orderCount; i++) {
                var orderId = fr.ToInt("order-" + i);
                var order = MaisLifeModel.DatabaseContext.Model.pedido.FirstOrDefault(f => f.id == orderId);

                var x = new mapa_pedido()
                {
                    mapaentrega = this.Map,
                    pedido1 = order                    
                };

                order.status = "Em trânsito";
                this.Map.mapa_pedido.Add(x);

            }

            if (this.Map.observacao == null)
                this.Map.observacao = "Nenhuma observação.";

            MaisLifeModel.DatabaseContext.Model.mapaentrega.Add(this.Map);
            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public int DoEdit()
        {
            var fr = new FastRequest(this.Request);
            return fr.ToInt("item");
        }

        public int DoSearch()
        {
            var fr = new FastRequest(this.Request);
            var id = fr.ToInt("map");

            var map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(f => f.id == id);
            if (map != null)
                return id;
            else
                return 0;
        }

        public void DoConfirm() {
            var fr = new FastRequest(this.Request);
            var mapId = fr.ToInt("mapId");
            var map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(f => f.id == mapId);

            foreach (var x in map.mapa_pedido) {
                x.pedido1.status = "Entregue";
            }

            MaisLifeModel.DatabaseContext.Model.mapaentrega.Remove(map);
            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();

        }

        public void DoRemove() {
            var fr = new FastRequest(this.Request);

            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(p => p.id == id);

                foreach (var x in map.mapa_pedido)
                {
                    x.pedido1.status = "Em aberto";
                }

                MaisLifeModel.DatabaseContext.Model.mapaentrega.Remove(map);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public MapaAdapter DoPrint(int id)
        {
            var map = MaisLifeModel.DatabaseContext.Model.mapaentrega.FirstOrDefault(p => p.id == id);
            return new MapaAdapter().ToMapaAdapter(map);
        }
    }
}