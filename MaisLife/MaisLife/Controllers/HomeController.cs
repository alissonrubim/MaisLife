using MaisLife.Helper;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            Sessions.EditProductInShoppingCart(2, 0);

            HttpCookie cookie = Request.Cookies["shoppingCartMaisLife"];

            return View();
        }

        public ActionResult Produtos()
        {
            var products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Produto(int id)
        {
            var products = ConfigDB.Model.Produtos;
            var product = products.FirstOrDefault(f => f.Id == id);
            if (product != null)
            {
                ViewBag.Products = products.ToList();
                ViewBag.Product = product;
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Carrinho(int id, int local)
        {

            // CHECAMOS DE FOI PASSADO ALGUM PRODUTO PARA A PÁGINA
            if (id > 0)
            {
                var product = ConfigDB.Model.Produtos.FirstOrDefault(f => f.Id == id);
                // CHECAMOS DE O PRODUTO PASSADO EXISTE
                if (product != null)
                {
                    Usuario user = (Usuario)HttpContext.Session["user"];
                    // CHECAMOS DE HÁ ALGUM USUÁRIO LOGADO
                    if (user != null)
                    {
                        Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                        // CHECAMOS SE HÁ ALGUM CARRINHO ATIVO
                        if (cart == null)
                        {
                            cart = new Carrinho()
                            {
                                Usuario1 = user,
                                Status = "Ativo"
                            };

                            ConfigDB.Model.Add(cart);
                            if (ConfigDB.Model.HasChanges)
                                ConfigDB.Model.SaveChanges();
                        }
                        
                        Carrinho_produto rel = cart.checkProduct(product);
                        // CHECAMOS SE O PRODUTO JÁ ESTÁ NO CARRINHO
                        if (rel == null)
                        {
                            rel = new Carrinho_produto()
                            {
                                Produto1 = product,
                                Carrinho1 = cart,
                                Quantidade = 1
                            };
                        }
                        else
                            rel.Quantidade++;

                        // SALVA/EDITA RELAÇÃO NO BANCO DE DADOS
                        ConfigDB.Model.Add(rel);
                        if (ConfigDB.Model.HasChanges)
                            ConfigDB.Model.SaveChanges();


                        ViewBag.Cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                        ViewBag.User = user;

                    }

                }
            }
            else
            {
                Usuario user = (Usuario)HttpContext.Session["user"];
                 // CHECAMOS DE HÁ ALGUM USUÁRIO LOGADO
                if (user != null)                {
                    Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                    if (cart == null)
                    {
                        cart = new Carrinho()
                        {
                            Usuario1 = user,
                            Status = "Ativo"
                        };

                        ConfigDB.Model.Add(cart);
                        if (ConfigDB.Model.HasChanges)
                            ConfigDB.Model.SaveChanges();
                    }

                    ViewBag.Cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                    ViewBag.User = user;
                }
                
            }

            if ( local != null && local != 0 )
            {
                ViewBag.Local = ConfigDB.Model.Bairros.FirstOrDefault(f => f.Id == local); ;
            }

            return View();
        }

        public ActionResult AlterarCarrinho()
        {

            var amount = Convert.ToInt32(Request.Form["amount"]);            

            if (amount > 0)
            {

                Usuario user = (Usuario)HttpContext.Session["user"];
                if (user != null) {
                    Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                    if (cart != null)
                    {
                        for (var i = 1; i <= amount; i++)
                        {
                            var qtd = Convert.ToInt32(Request.Form["qtd-" + i]);
                            var id = Convert.ToInt32(Request.Form["hidden-" + i]);

                            var rel = cart.Carrinho_produtos.FirstOrDefault(f => f.Id == id);
                            rel.Quantidade = qtd;

                            if (rel.Quantidade <= 0)
                            {
                                cart.Carrinho_produtos.Remove(rel);
                                ConfigDB.Model.Delete(rel);
                                if (ConfigDB.Model.HasChanges)
                                    ConfigDB.Model.SaveChanges();
                            }
                           

                        }

                        ConfigDB.Model.Add(cart);
                        if (ConfigDB.Model.HasChanges)
                            ConfigDB.Model.SaveChanges();

                    }
                   
                }

            }


            return RedirectToAction("Carrinho", "Home", new { id = 0, local = 0 });
        }

        public ActionResult EnderecoEPagamento() 
        {
            
            Usuario user = (Usuario)HttpContext.Session["user"];
            ViewBag.User = user;
            
            Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
            ViewBag.Cart = cart;
            return View();
        }

        public ActionResult CalcularEntrega()        {
            
            var localString = Request.Form["local"];
            int local = 0;

            if (localString != "")
            {
                local = ConfigDB.Model.Bairros.FirstOrDefault(f => f.Nome == localString).Id;
            }            

            return RedirectToAction("Carrinho", "Home", new { id = 0, local = local});

        }

        public ActionResult NovoEndereco(EnderecoAdapter endereco)
        {
            Bairro bairro = ConfigDB.Model.Bairros.FirstOrDefault(f => f.Nome.ToLower() == endereco.Bairro.ToLower());
            if (bairro != null)
            {
                Usuario user = (Usuario)HttpContext.Session["user"];

                endereco.Usuario = user.Id;
                endereco.Pais = "Brasil";
                endereco.Estado = "MG";

                var end = endereco.ToEndereco();
                end.Bairro1 = bairro;

                ConfigDB.Model.Add(end);
                if (ConfigDB.Model.HasChanges)
                    ConfigDB.Model.SaveChanges();
            }           

            return RedirectToAction("EnderecoEPagamento", "Home");
        }

        public ActionResult CreateContact(ContatoAdapter contato)
        {
            ConfigDB.Model.Add(contato.ToContato());
            if (ConfigDB.Model.HasChanges)
            {
                ConfigDB.Model.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateUser(UsuarioAdapter user)
        {
            if (Validation.ExistingEmailValidation(user))
            {
                ConfigDB.Model.Add(user.ToUsuario());
                if (ConfigDB.Model.HasChanges)
                {
                    ConfigDB.Model.SaveChanges();
                    Sessions.CreateCookie(user.ToUsuario(), false);
                }
            }
            else
            {
                TempData["MessageErroRegister"] = "E-mail já cadastrado";
            }
            return RedirectToAction("Index");
        }

        public ActionResult LoginUser(UsuarioAdapter user)
        {
            if (Validation.ValidationLogin(user))
            {
                Sessions.CreateCookie(user.ToUsuario(), false);
            }
            else
            {
                TempData["MessageErroLogin"] = "E-mail ou senha incorretos";
            }
            return RedirectToAction("Index");
        }

        public ActionResult LogoutUser()
        {
            Sessions.Logout();

            return RedirectToAction("Index");
        }

    }
}