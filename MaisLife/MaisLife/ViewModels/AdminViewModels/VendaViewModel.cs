using MaisLife.Helper;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.ViewModels.AdminViewModels
{
    public class VendaViewModel{

        public HttpRequestBase Request { get; set; }

        public VendaViewModel(HttpRequestBase Request) {
            this.Request = Request;
        }

        public List<pedido> DoSearch(int situation) {
            var fr = new FastRequest(this.Request);

            if (situation == 0)
            {
                return MaisLifeModel.DatabaseContext.Model.pedido.ToList().OrderBy(w => w.previsao_entrega).ToList();
            }
            else { 
                
                var allOrders = MaisLifeModel.DatabaseContext.Model.pedido.ToList().OrderBy(w => w.previsao_entrega).ToList();
                var num = fr.ToInt("search-num");
                if (num > 0)
                    allOrders = allOrders.Where(w => w.id == num).ToList();

                var source = fr.ToString("search-source");
                if (source == "site")
                    allOrders = allOrders.Where(w => w.origem == "Site").ToList();
                else if (source == "external")
                    allOrders = allOrders.Where(w => w.origem == "Vendedor").ToList();               

                var type = fr.ToString("search-type");
                if (type == "default")
                    allOrders = allOrders.Where(w => w.tipo == "Venda").ToList();
                else if (type == "change")
                    allOrders = allOrders.Where(w => w.tipo == "Troca").ToList();
                else if (type == "bonus")
                    allOrders = allOrders.Where(w => w.tipo == "Bonificado").ToList();
                else if (type == "merchan")
                    allOrders = allOrders.Where(w => w.tipo == "Merchandising").ToList();

                var payment = fr.ToString("search-payment");
                if (payment == "cash")
                    allOrders = allOrders.Where(w => w.metodo == "A vista").ToList();
                else if (payment == "deadline")
                    allOrders = allOrders.Where(w => w.metodo == "Prazo").ToList();
                else if (payment == "billet")
                    allOrders = allOrders.Where(w => w.metodo == "Boleto").ToList();
                else if (payment == "roll")
                    allOrders = allOrders.Where(w => w.metodo == "Consignado").ToList();

                var discount = fr.ToString("search-discount");
                if (discount == "Com desconto")
                    allOrders = allOrders.Where(w => w.desconto > 0).ToList();
                else if (discount == "Sem desconto")
                    allOrders = allOrders.Where(w => w.desconto == 0).ToList();

                var minValueString = fr.ToString("search-minusValue");
                var minValue = Converter.ConvertMoney(minValueString);
                if (minValue > 0) 
                {
                    allOrders = allOrders.Where(w => w.valor >= minValue).ToList();
                }

                var maxValueString = fr.ToString("search-maximusValue");
                var maxValue = Converter.ConvertMoney(maxValueString);
                if (maxValue > 0)
                {
                    allOrders = allOrders.Where(w => w.valor <= maxValue).ToList();
                }

                var seller = fr.ToInt("search-seller");
                if (seller > 0)
                {
                    allOrders = allOrders.Where(w => w.origem == "Vendedor" && w.usuario1.id == seller).ToList();
                }

                var client = fr.ToInt("search-client");
                if (client > 0)
                {
                    allOrders = allOrders.Where(w => w.origem == "Site" && w.usuario1.id == client).ToList();
                }

                var external = fr.ToInt("search-external");
                if (external > 0)
                {
                    allOrders = allOrders.Where(w => w.origem == "Vendedor" && w.usuario_externo1.id == external).ToList();
                }

                var product = fr.ToInt("search-product");
                if (product > 0)
                {
                    var supportList = MaisLifeModel.DatabaseContext.Model.pedido.ToList();
                    var have = false;
                    foreach (var order in supportList) {
                        foreach (var x in order.carrinho1.carrinho_produto) {
                            if (x.produto1.id == product) {
                                have = true;
                            }
                        }

                        if ( !have )
                            allOrders.Remove(order);

                        have = false;
                    }                    
                }

                var local = fr.ToInt("search-local");
                if (local > 0)
                {
                    allOrders = allOrders.Where(w => w.endereco1.bairro1.id == local).ToList();
                }

                var startDate = fr.ToString("search-startDate");
                if (startDate != "") {
                    var date = DateTime.ParseExact(startDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.data >= date).ToList();
                }

                var endDate = fr.ToString("search-endDate");
                if (endDate != "")
                {
                    var date = DateTime.ParseExact(endDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.data <= date).ToList();
                }

                var startShippingDate = fr.ToString("search-startShippingDate");
                if (startShippingDate != "")
                {
                    var date = DateTime.ParseExact(startShippingDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.previsao_entrega >= date).ToList();
                }

                var endShippoingDate = fr.ToString("search-endShippingDate");
                if (endShippoingDate != "")
                {
                    var date = DateTime.ParseExact(endShippoingDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.previsao_entrega <= date).ToList();
                }

                return allOrders;

            }


        }

    }
}