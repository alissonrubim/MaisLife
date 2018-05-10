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
        public Mapaentrega Map { get; set; }

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

            if (this.Map.Id > 0) { 
                var newMap = this.Map;
                this.Map = MaisLifeModel.DatabaseContext.Model.Mapaentrega.FirstOrDefault(f => f.Id == this.Map.Id);
                this.Map.Observacao = newMap.Observacao;
                this.Map.Data_entrega = newMap.Data_entrega;

                foreach (var x in this.Map.Mapa_pedidos) {
                    x.Pedido1.Status = "Em aberto";
                }
                
                foreach(Mapa_pedido mp in this.Map.Mapa_pedidos)
                    MaisLifeModel.DatabaseContext.Model.Mapa_pedido.Remove(mp);
            }
            
            this.Map.Mapa_pedidos = new List<Mapa_pedido>();

            var orderCount = fr.ToInt("orderCount");
            for (var i = 1; i <= orderCount; i++) {
                var orderId = fr.ToInt("order-" + i);
                var order = MaisLifeModel.DatabaseContext.Model.Pedido.FirstOrDefault(f => f.Id == orderId);

                var x = new Mapa_pedido()
                {
                    Mapaentrega = this.Map,
                    Pedido1 = order                    
                };

                order.Status = "Em trânsito";
                this.Map.Mapa_pedidos.Add(x);

            }

            if (this.Map.Observacao == null)
                this.Map.Observacao = "Nenhuma observação.";

            MaisLifeModel.DatabaseContext.Model.Mapaentrega.Add(this.Map);
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

            var map = MaisLifeModel.DatabaseContext.Model.Mapaentrega.FirstOrDefault(f => f.Id == id);
            if (map != null)
                return id;
            else
                return 0;
        }

        public void DoConfirm() {
            var fr = new FastRequest(this.Request);
            var mapId = fr.ToInt("mapId");
            var map = MaisLifeModel.DatabaseContext.Model.Mapaentrega.FirstOrDefault(f => f.Id == mapId);

            foreach (var x in map.Mapa_pedidos) {
                x.Pedido1.Status = "Entregue";
            }

            MaisLifeModel.DatabaseContext.Model.Mapaentrega.Remove(map);
            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();

        }

        public void DoRemove() {
            var fr = new FastRequest(this.Request);

            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var map = MaisLifeModel.DatabaseContext.Model.Mapaentrega.FirstOrDefault(p => p.Id == id);

                foreach (var x in map.Mapa_pedidos)
                {
                    x.Pedido1.Status = "Em aberto";
                }

                MaisLifeModel.DatabaseContext.Model.Mapaentrega.Remove(map);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public MapaAdapter DoPrint(int id)
        {
            var map = MaisLifeModel.DatabaseContext.Model.Mapaentrega.FirstOrDefault(p => p.Id == id);
            return new MapaAdapter().ToMapaAdapter(map);
        }
    }
}