using MaisLife.Helper;
using MaisLifeModel;
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
            
            this.Map.Mapa_pedidos = new List<Mapa_pedido>();

            var orderCount = fr.ToInt("orderCount");
            for (var i = 1; i <= orderCount; i++) {
                var orderId = fr.ToInt("order-" + i);
                var order = ConfigDB.Model.Pedidos.FirstOrDefault(f => f.Id == orderId);

                var x = new Mapa_pedido()
                {
                    Mapaentrega = this.Map,
                    Pedido1 = order                    
                };

                this.Map.Mapa_pedidos.Add(x);

            }

            ConfigDB.Model.Add(this.Map);
            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
        }
    }
}