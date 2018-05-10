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

        public List<Pedido> DoSearch(int situation) {
            var fr = new FastRequest(this.Request);

            if (situation == 0)
            {
                return MaisLifeModel.DatabaseContext.Model.Pedido.ToList().OrderBy(w => w.Previsao_entrega).ToList();
            }
            else { 
                
                var allOrders = MaisLifeModel.DatabaseContext.Model.Pedido.ToList().OrderBy(w => w.Previsao_entrega).ToList();
                var num = fr.ToInt("search-num");
                if (num > 0)
                    allOrders = allOrders.Where(w => w.Id == num).ToList();

                var source = fr.ToString("search-source");
                if (source == "site")
                    allOrders = allOrders.Where(w => w.Origem == "Site").ToList();
                else if (source == "external")
                    allOrders = allOrders.Where(w => w.Origem == "Vendedor").ToList();               

                var type = fr.ToString("search-type");
                if (type == "default")
                    allOrders = allOrders.Where(w => w.Tipo == "Venda").ToList();
                else if (type == "change")
                    allOrders = allOrders.Where(w => w.Tipo == "Troca").ToList();
                else if (type == "bonus")
                    allOrders = allOrders.Where(w => w.Tipo == "Bonificado").ToList();
                else if (type == "merchan")
                    allOrders = allOrders.Where(w => w.Tipo == "Merchandising").ToList();

                var payment = fr.ToString("search-payment");
                if (payment == "cash")
                    allOrders = allOrders.Where(w => w.Metodo == "A vista").ToList();
                else if (payment == "deadline")
                    allOrders = allOrders.Where(w => w.Metodo == "Prazo").ToList();
                else if (payment == "billet")
                    allOrders = allOrders.Where(w => w.Metodo == "Boleto").ToList();
                else if (payment == "roll")
                    allOrders = allOrders.Where(w => w.Metodo == "Consignado").ToList();

                var discount = fr.ToString("search-discount");
                if (discount == "Com desconto")
                    allOrders = allOrders.Where(w => w.Desconto > 0).ToList();
                else if (discount == "Sem desconto")
                    allOrders = allOrders.Where(w => w.Desconto == 0).ToList();

                var minValueString = fr.ToString("search-minusValue");
                var minValue = Converter.ConvertMoney(minValueString);
                if (minValue > 0) 
                {
                    allOrders = allOrders.Where(w => w.Valor >= minValue).ToList();
                }

                var maxValueString = fr.ToString("search-maximusValue");
                var maxValue = Converter.ConvertMoney(maxValueString);
                if (maxValue > 0)
                {
                    allOrders = allOrders.Where(w => w.Valor <= maxValue).ToList();
                }

                var seller = fr.ToInt("search-seller");
                if (seller > 0)
                {
                    allOrders = allOrders.Where(w => w.Origem == "Vendedor" && w.Usuario1.Id == seller).ToList();
                }

                var client = fr.ToInt("search-client");
                if (client > 0)
                {
                    allOrders = allOrders.Where(w => w.Origem == "Site" && w.Usuario1.Id == client).ToList();
                }

                var external = fr.ToInt("search-external");
                if (external > 0)
                {
                    allOrders = allOrders.Where(w => w.Origem == "Vendedor" && w.Usuario_externo1.Id == external).ToList();
                }

                var product = fr.ToInt("search-product");
                if (product > 0)
                {
                    var supportList = MaisLifeModel.DatabaseContext.Model.Pedido.ToList();
                    var have = false;
                    foreach (var order in supportList) {
                        foreach (var x in order.Carrinho1.Carrinho_produtos) {
                            if (x.Produto1.Id == product) {
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
                    allOrders = allOrders.Where(w => w.Endereco1.Bairro1.Id == local).ToList();
                }

                var startDate = fr.ToString("search-startDate");
                if (startDate != "") {
                    var date = DateTime.ParseExact(startDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.Data >= date).ToList();
                }

                var endDate = fr.ToString("search-endDate");
                if (endDate != "")
                {
                    var date = DateTime.ParseExact(endDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.Data <= date).ToList();
                }

                var startShippingDate = fr.ToString("search-startShippingDate");
                if (startShippingDate != "")
                {
                    var date = DateTime.ParseExact(startShippingDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.Previsao_entrega >= date).ToList();
                }

                var endShippoingDate = fr.ToString("search-endShippingDate");
                if (endShippoingDate != "")
                {
                    var date = DateTime.ParseExact(endShippoingDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    allOrders = allOrders.Where(w => w.Previsao_entrega <= date).ToList();
                }

                return allOrders;

            }


        }

    }
}