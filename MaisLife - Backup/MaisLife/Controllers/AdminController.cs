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
  
        public ActionResult Parceiros(int id = 0)
        {
            ParceiroAdapter adapter = null;
            
            if (id > 0) {
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
                adapter = new ParceiroAdapter().ToParceiroAdapter(patner);  
            }
            
            ViewBag.Patners = ConfigDB.Model.Parceiros.ToList();
            if (id > 0)
                return View(adapter);
            else
                return View();
        }

        public ActionResult Produtos(int id = 0)
        {
            ProdutoAdapter adapter = null;
            
            if (id > 0) {
                var produto = ConfigDB.Model.Produtos.FirstOrDefault(f => f.Id == id);
                adapter = new ProdutoAdapter().ToProdutoAdapter(produto);
            }
            
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            if (id > 0)
                return View(adapter);
            else
                return View();
        }       
       
        public ActionResult VendasExternas(int id = 0)
        {
            PedidoAdapter adapter = null;
            Pedido external = null;
            if (id > 0) {
                external = ConfigDB.Model.Pedidos.FirstOrDefault(o => o.Id == id);
                ViewBag.Cart = external.Carrinho1;
                adapter = new PedidoAdapter().ToPedidoAdapter(external);
            }
            
            var logged = (Usuario) HttpContext.Session["user"];

            if ( logged.Permissao < 2 )
                ViewBag.Orders = ConfigDB.Model.Pedidos.Where(o => o.Usuario == logged.Id && o.Origem == "Vendedor").ToList();
            else
                ViewBag.Orders = ConfigDB.Model.Pedidos.ToList();
            ViewBag.User = logged;
            ViewBag.OutsideClients = ConfigDB.Model.Usuario_externos.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Sellers = ConfigDB.Model.Usuarios.Where(u => u.Permissao >= 1).ToList();
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
           

            if (id > 0)
                return View(adapter);
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
            var order = adapter.ToPedido();
            order.Usuario1 = ConfigDB.Model.Usuarios.FirstOrDefault(f => f.Id == order.Usuario1.Id);
           
            var user = (Usuario) HttpContext.Session["user"];
            
            var amount = Convert.ToInt32(Request.Form["product-amount"]);
            if (amount > 0)
            {
                decimal orderValue = 0;
                
                var cart = new Carrinho()
                {
                    Carrinho_produtos = new List<Carrinho_produto>(),
                    Status = "Fechado",
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
                        Quantidade = productcount,
                        Carrinho1 = cart
                    };

                    cart.Carrinho_produtos.Add(rel);

                    orderValue += (decimal) product.Preco * productcount; 
                }

                order.Carrinho1 = cart;
        
                if ( order.Usuario_externo1.Idusuario > 0 )
                    order.Usuario_externo1 = ConfigDB.Model.Usuario_externos.FirstOrDefault(c => c.Idusuario == order.Usuario_externo1.Idusuario);
                                
                order.Origem = "Vendedor";
                order.Data = DateTime.Now;               
                order.Endereco1 = order.Usuario_externo1.Endereco1;
                order.Status = "Enviado";
                order.Valor = orderValue;
               

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

            return RedirectToAction("Parceiros", new { id = id });
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

            return RedirectToAction("Produtos", new { id = id });
        }

        [HttpPost]
        public ActionResult EditarVendaExterna()
        {
            var id = Convert.ToInt32(Request.Form["item"]);
            var user = (Usuario) HttpContext.Session["user"];
            var external = ConfigDB.Model.Pedidos.FirstOrDefault(o => o.Id == id);           
            if ( user.Permissao < 2 ){
                if ( external.Status == "Enviado" ){
                    return RedirectToAction("VendasExternas", new { id = id} );
                }else
                    return RedirectToAction("VendasExternas");
            }else{
                return RedirectToAction("VendasExternas", new { id = id});                    
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