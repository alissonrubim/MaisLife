using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

        public ActionResult Produtos(ProdutoAdapter product)
        {
            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();
            ViewBag.Products = ConfigDB.Model.Produtos.ToList();
            if (product != null)
                return View(product);
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
    }
}