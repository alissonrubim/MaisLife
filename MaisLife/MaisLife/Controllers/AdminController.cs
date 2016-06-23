using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace MaisLife.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
  
        public ActionResult Parceiros(ParceiroAdapter patner = null)
        {
            ViewBag.Patners = ConfigDB.Model.Parceiros.ToList();
            if ( patner != null )
                return View(patner);
            else
                return View();
        }

        public ActionResult Produtos(ProdutoAdapter product = null)
        {
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            if (product != null)
                return View(product);
            else
                return View();
        }
       
        public ActionResult VendasExternas(PedidoAdapter order = null)
        {
            var logged = (Usuario) HttpContext.Session["user"];

            ViewBag.Orders = ConfigDB.Model.Pedidos.Where(o => o.Usuario == logged.Id && o.Origem == "Vendedor").ToList();
            ViewBag.User = logged;
            ViewBag.OutsideClients = ConfigDB.Model.Usuario_externos.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(u => u.Permissao >= 1).ToList();
            if (order != null)               
                return View(order);
            else
                return View();
        }

        [HttpPost]
        public ActionResult ManagerPatner(ParceiroAdapter adapter, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Uploads"), file.FileName);
                    file.SaveAs(path);
                    adapter.Imagem = file.FileName;
                }           
            }
             

            if (adapter.Id == 0)
            {
                ConfigDB.Model.Add(adapter.ToParceiro());
            }
            else
            {
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(f => f.Id == adapter.Id);
                patner.Nome = adapter.Nome;
                patner.Enderec = adapter.Endereco;
                patner.Telefone = adapter.Telefone;
                patner.Site = adapter.Site;
                patner.Facebook = adapter.Facebook;
                patner.Imagem = adapter.Imagem;
                ConfigDB.Model.Add(patner);
            }
            
            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
            
            return RedirectToAction("Parceiros");
        }

        [HttpPost]
        public ActionResult ManagerProduct(ProdutoAdapter adapter, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Uploads"), file.FileName);
                    file.SaveAs(path);
                    adapter.Imagem = file.FileName;
                }
            }


            if (adapter.Id == 0)
            {
                var amount = Convert.ToInt32(Request.Form["delivery-amount"]);
                var product = adapter.ToProduto();
                if (amount > 0)
                {
                    product.Produto_bairros = new List<Produto_bairro>();
                    for (var i = 1; i <= amount; i++)
                    {
                        var local = Convert.ToInt32(Request.Form["delivery-local-" + i]);
                        var tax = Convert.ToDecimal(Request.Form["delivery-tax-" + i]);

                        var rel = new Produto_bairro()
                        {
                            Bairro1 = ConfigDB.Model.Bairros.FirstOrDefault(b => b.Id == local),
                            Produto1 = product,
                            Taxa = tax
                        };

                        product.Produto_bairros.Add(rel);
                        
                    }
                }


                ConfigDB.Model.Add(product);
                
            }
            else
            {                
                var product = ConfigDB.Model.Produtos.FirstOrDefault(f => f.Id == adapter.Id);
                foreach (var rel in product.Produto_bairros)
                {
                    ConfigDB.Model.Delete(rel);
                }

                var amount = Convert.ToInt32(Request.Form["delivery-amount"]);               
                if (amount > 0)
                {                   
                    for (var i = 1; i <= amount; i++)
                    {
                        var local = Convert.ToInt32(Request.Form["delivery-local-" + i]);
                        var tax = Convert.ToDecimal(Request.Form["delivery-tax-" + i]);

                        var rel = new Produto_bairro()
                        {
                            Bairro1 = ConfigDB.Model.Bairros.FirstOrDefault(b => b.Id == local),
                            Produto1 = product,
                            Taxa = tax
                        };

                        product.Produto_bairros.Add(rel);

                    }
                }

                product.Nome = adapter.Nome;
                product.Descricao = adapter.Descricao;
                product.Preco = adapter.Preco;
                product.Unidade = adapter.Unidade;
                ConfigDB.Model.Add(product);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();

            return RedirectToAction("Produtos");
        }

        [HttpPost]
        public ActionResult ManagerExternalOrder(PedidoAdapter adapter)
        {
            var user = (Usuario) HttpContext.Session["user"];
            
            var amount = Convert.ToInt32(Request.Form["product-amount"]);
            if (amount > 0)
            {
                decimal orderValue = 0;
                
                var cart = new Carrinho()
                {
                    Carrinho_produtos = new List<Carrinho_produto>(),
                    Status = "Ativo",
                    Usuario1 = user
                };
                
                for (var i = 1; i <= amount; i++)
                {
                    var productid = Convert.ToInt32(Request.Form["product-" + i]);
                    var productcount = Convert.ToInt32(Request.Form["product-count-" + i]);

                    var product = ConfigDB.Model.Produtos.FirstOrDefault(p => p.Id == productid);

                    var rel = new Carrinho_produto()
                    {
                        Produto1 = product,
                        Quantidade = productcount
                    };

                    cart.Carrinho_produtos.Add(rel);

                    orderValue += (decimal) product.Preco * productcount; 
                }

                Usuario_externo client;

                var method = Request.Form["client-payment"];
                if (method.Contains("Car"))
                    method = "Cartão";
                else
                    method = "Dinheiro";

                var paid = Convert.ToDecimal(Request.Form["client-pay"]);
                
                var clientid = Convert.ToInt32(Request.Form["client"]);
                if ( clientid > 0 ){
                    client = ConfigDB.Model.Usuario_externos.FirstOrDefault(c => c.Idusuario == clientid);
                }else{
                    
                    var clientName = Request.Form["client-name"];
                    var clientContact = Request.Form["client-phone"];
                    var clientDoc = Request.Form["client-doc"];

                    var clientCity = Request.Form["client-city"];
                    var clientLocal = Request.Form["client-local"];
                    var clientStreet = Request.Form["client-street"];
                    var clientNumber = Request.Form["client-number"];
  
                    var clientAddress = new Endereco(){
                        Pais = "Brasil",
                        Estado = "MG",
                        Cidade = clientCity,
                        Bairro1 = ConfigDB.Model.Bairros.FirstOrDefault(b => b.Nome == clientLocal),
                        Rua = clientStreet,
                        Numero = clientNumber,
                        Usuario = 1
                    };

                    client = new Usuario_externo(){
                        Nome = clientName,
                        Telefone = clientContact,
                        Documento = clientDoc,
                        Endereco1 = clientAddress
                    };

                }

                var order = new Pedido()
                {
                    Carrinho1 = cart,
                    Origem = "Vendedor",
                    Usuario1 = user,
                    Data = DateTime.Now,
                    Endereco1 = client.Endereco1,
                    Metodo = method,
                    Pago = paid,
                    Status = "Enviado",
                    Usuario_externo1 = client,
                    Valor = orderValue
                };

                ConfigDB.Model.Add(order);
                if ( ConfigDB.Model.HasChanges )
                    ConfigDB.Model.SaveChanges();

            }

            return RedirectToAction("VendasExternas");
        }

        [HttpPost]
        public ActionResult RemoverParceiro()
        {
            var count = Convert.ToInt32(Request.Form["count"]);
            for (var i = 1; i <= count; i++ )
            {
                var id = Convert.ToInt32(Request.Form["item-" + i]);
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
                ConfigDB.Model.Delete(patner);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();

            return RedirectToAction("Parceiros");
        }

        [HttpPost]
        public ActionResult EditarParceiro()
        {
            var id = Convert.ToInt32(Request.Form["item"]);

            var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
            var adapter = new ParceiroAdapter().ToParceiroAdapter(patner);

            return RedirectToAction("Parceiros", new RouteValueDictionary(adapter));
        }

        [HttpPost]
        public ActionResult RemoverProduto()
        {
            var count = Convert.ToInt32(Request.Form["count"]);
            for (var i = 1; i <= count; i++)
            {
                var id = Convert.ToInt32(Request.Form["item-" + i]);
                var product = ConfigDB.Model.Produtos.FirstOrDefault(p => p.Id == id);
                ConfigDB.Model.Delete(product);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();

            return RedirectToAction("Produtos");
        }

        [HttpPost]
        public ActionResult EditarProduto()
        {
            var id = Convert.ToInt32(Request.Form["item"]);

            var product = ConfigDB.Model.Produtos.FirstOrDefault(p => p.Id == id);
            var adapter = new ProdutoAdapter().ToProdutoAdapter(product);

            return RedirectToAction("Produtos", new RouteValueDictionary(adapter));
        }

        [HttpPost]
        public ActionResult EditarVendaExterna()
        {
            var id = Convert.ToInt32(Request.Form["item"]);

            var user = (Usuario) HttpContext.Session["user"];

            var external = ConfigDB.Model.Pedidos.FirstOrDefault(o => o.Id == id);
            var adapter = new PedidoAdapter().ToPedidoAdapter(external);
            if ( user.Permissao < 2 ){
                if ( external.Status == "Enviado" ){
                    return RedirectToAction("VendasExternas", new RouteValueDictionary(adapter));
                }else
                    return RedirectToAction("VendasExternas");
            }else{
                return RedirectToAction("VendasExternas", new RouteValueDictionary(adapter));                    
            }
            
        }

        public string ExternalUsersAjax()
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            var externalUser = ConfigDB.Model.Usuario_externos.FirstOrDefault(eu => eu.Idusuario == id);
            
            // ANULAR REFERÊNCIAS
            externalUser.Endereco1.Bairro1.Enderecos = null;
            externalUser.Endereco1.Usuario_externos = null;
            externalUser.Endereco1.Usuario1 = null;           
            externalUser.Pedidos = null;
            externalUser.Endereco1.Pedidos = null;
            
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(externalUser);
        }

        public string ExternalUsersProductsAjax() {
            var id = Convert.ToInt32(Request.Form["id"]);
            var product = ConfigDB.Model.Produtos.FirstOrDefault(p => p.Id == id);

            // ANULAR REFERÊNCIAS
            product.Produto_bairros = null;
            product.Carrinho_produtos = null;
            
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(product);
        }
    }
}