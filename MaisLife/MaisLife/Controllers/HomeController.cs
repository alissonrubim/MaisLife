using MaisLife.Helper;
using MaisLife.Models;
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
            Injections.LayoutInjection(this);
            return View();
        }

        public ActionResult Produtos()
        {           
            var products = ConfigDB.Model.Produtos.ToList();
            ViewBag.Products = products;
            Injections.LayoutInjection(this);
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
                Injections.LayoutInjection(this);
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

                    }
                    else
                    {
                        Produto produto = new Produto(){
                            Id = id
                        };

                        Sessions.AddProductInShoppingCart(produto);                        
                    }

                }
            }
            else
            {
                Usuario user = (Usuario)HttpContext.Session["user"];
                 // CHECAMOS DE HÁ ALGUM USUÁRIO LOGADO
                if (user != null)
                {
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
                }
                else
                {
                    ViewBag.Cart = Sessions.FindShoppingCart();
                }
                
            }

            if ( local != 0 )
            {
                ViewBag.Local = ConfigDB.Model.Bairros.FirstOrDefault(f => f.Id == local); ;
            }

            ViewBag.Locals = ConfigDB.Model.Bairros.ToList();

            Injections.LayoutInjection(this);
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
                else
                {
                    for (var i = 1; i <= amount; i++)
                    {
                        var qtd = Convert.ToInt32(Request.Form["qtd-" + i]);
                        var id = Convert.ToInt32(Request.Form["hidden-" + i]);

                        Sessions.EditProductInShoppingCart(id, qtd);
                    }
                }

            }


            return RedirectToAction("Carrinho", "Home", new { id = 0, local = 0 });
        }

        public ActionResult EnderecoEPagamento() 
        {
           
            Usuario user = (Usuario)HttpContext.Session["user"];

            if(user != null)
            {
                Injections.LayoutInjection(this);
                return View();
            }

            return RedirectToAction("CadastroELogin", "Home");
        }

        public ActionResult FinalizarPedido()
        {           
            try { 
                var id = Convert.ToInt32(Request.Form["address"]);
                Endereco address = ConfigDB.Model.Enderecos.FirstOrDefault(f => f.Id == id);
                if (address != null)
                {
                    Usuario user = (Usuario)HttpContext.Session["user"];
                    Carrinho cart = user.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");

                    var metodo = Request.Form["payMethod"];
                    var payValue = Convert.ToDecimal(Request.Form["payValue"]);
                    if (payValue < cart.Total(address.Bairro1.Taxa) && metodo == "Dinheiro")
                    {
                        TempData["Error"] = "Valor digitado é menor que o valor total da compra.";
                        return RedirectToAction("EnderecoEPagamento", "Home");
                    }
                    else
                    {
                        Pedido order = new Pedido()
                        {
                            Usuario1 = user,
                            Valor = cart.Total(address.Bairro1.Taxa),
                            Carrinho1 = cart,
                            Endereco1 = address,
                            Pago = payValue,
                            Metodo = metodo,
                            Status = "Enviado",
                            Data = DateTime.Now
                        };

                        ConfigDB.Model.Add(order);

                        cart.Status = "Fechado";
                        ConfigDB.Model.Add(cart);

                        Carrinho newCart = new Carrinho()
                        {
                            Usuario1 = user,
                            Status = "Ativo"
                        };

                        ConfigDB.Model.Add(newCart);

                        if (ConfigDB.Model.HasChanges)
                            ConfigDB.Model.SaveChanges();
                        
                        return RedirectToAction("Perfil", "Home");
                    }

                }
                else
                {
                    TempData["Error"] = "Endereço não informado.";                   
                    return RedirectToAction("EnderecoEPagamento", "Home");
                }
                    


            }catch(Exception ex){
                TempData["Error"] = "Houve um erro na autenticação.";  
                return RedirectToAction("EnderecoEPagamento", "Home");
            }
            
        }
        
        public ActionResult CalcularEntrega()        {
            
            var localString = Request.Form["local"];
            int local = 0;

            if (localString != "0" && localString != "")
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

                if (user != null)
                {
                    endereco.Usuario = user.Id;
                    endereco.Pais = "Brasil";
                    endereco.Estado = "MG";

                    var end = endereco.ToEndereco();
                    end.Bairro1 = bairro;

                    ConfigDB.Model.Add(end);
                    if (ConfigDB.Model.HasChanges)
                        ConfigDB.Model.SaveChanges();
                }
                
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
            var usuario = Validation.ValidationLogin(user);
            if (usuario == null)
            {
                TempData["MessageErroLogin"] = "E-mail ou senha incorretos";
            }
            else
            {
                Sessions.CreateCookie(usuario, false);

                //RECUPERA LISTA DE RELAÇÕES DO COOKIE
                Carrinho cartCookie = Sessions.FindShoppingCart();

                if (cartCookie != null && cartCookie.Carrinho_produtos.Count() > 0)
                {
                    //RECUPERA CARRINHO BD
                    Carrinho cartBd = usuario.Carrinhos.FirstOrDefault(f => f.Status == "Ativo");
                    List<Carrinho_produto> relCartCookie = cartCookie.Carrinho_produtos.ToList();

                    //PASSANDO DADOS RELAÇÃO BD PARA LISTA AXU
                    List<Carrinho_produto> relCartAxu = new List<Carrinho_produto>();
                    foreach (Carrinho_produto relCokie in relCartCookie)
                    {
                        relCartAxu.Add(relCokie);
                    }
                    //SE EXISTE UM CARRINHO
                    if (cartBd != null)
                    {
                        //LISTA COM RELAÇOES DO BD
                        List<Carrinho_produto> relCartBd = cartBd.Carrinho_produtos.ToList();

                        //PERCORRE AS DUAS LSITAS PROCURANDO POR PRODUTOS IGUAIS
                        foreach (Carrinho_produto relacaoCookie in relCartCookie)
                        {
                            foreach (Carrinho_produto relacaoBd in relCartBd)
                            {
                                // SE É =
                                if (relacaoCookie.Produto1.Id == relacaoBd.Produto1.Id)
                                {
                                    //REMOVE DA LISTA AUXILIAR
                                    relCartAxu.Remove(relacaoCookie);
                                    //MDA JUNTA AS QUANTIDADES
                                    relacaoBd.Quantidade += relacaoCookie.Quantidade;
                                    
                                    if (ConfigDB.Model.HasChanges)
                                        ConfigDB.Model.SaveChanges();
                                    


                                }
                            }
                        }

                        //INSERE COMO NOVO PRODUTO OS QUE NÃO FORAM ENCONTRADOS NA RELAÇÃO EXISTENTE
                        if (relCartAxu.Count > 0)
                        {
                            foreach (Carrinho_produto cp in relCartAxu)
                            {
                                cp.Carrinho1 = cartBd;

                                ConfigDB.Model.Add(cp);

                                if (ConfigDB.Model.HasChanges)
                                    ConfigDB.Model.SaveChanges();
                            }
                        }
                    }
                    //CRIA E INSERE UM CARRINHO CASO NÃO EXISTA
                    else
                    {
                        cartBd = new Carrinho()
                        {
                            Usuario1 = usuario,
                            Status = "Ativo"
                        };

                        ConfigDB.Model.Add(cartBd);

                        if (ConfigDB.Model.HasChanges)
                            ConfigDB.Model.SaveChanges();

                        //BUSCA CARRINHO INSERIDO
                        Carrinho cartInserted = ConfigDB.Model.Carrinhos.FirstOrDefault
                            (f => f.Usuario1.Id == usuario.Id && f.Status == "Ativo");

                        //SETA CARRINHO DAS RELAÇÕES E INSERE RELAÇÃO
                        foreach (Carrinho_produto cp in relCartCookie)
                        {
                            cp.Carrinho1 = cartInserted;

                            ConfigDB.Model.Add(cp);

                            if (ConfigDB.Model.HasChanges)
                                ConfigDB.Model.SaveChanges();
                        }
                    }
                }
                Sessions.LimparCookie();
            }

            return RedirectToAction("Index");
        }

        public ActionResult LogoutUser()
        {
            Sessions.Logout();

            return RedirectToAction("Index");
        }

        public ActionResult CadastroELogin()
        {
            Injections.LayoutInjection(this);
            return View();
        }

        public ActionResult Perfil()
        {
            Injections.LayoutInjection(this);
            
            Usuario user = (Usuario)HttpContext.Session["user"];
            if (user == null)
            {
                return RedirectToAction("Index");
            }else{
                ViewBag.User = user;
                return View();
            }

            
        }
       

    }
}