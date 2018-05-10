using MaisLife.Helper;
using MaisLife.Models;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MaisLife.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {            
            Injections.LayoutInjection(this);
            ViewBag.patners = MaisLifeModel.DatabaseContext.Model.parceiro.ToList();

            var products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
            
            var slideProducts = new List<List<produto>>();
            var count = 0;
            var total = 0;

            var x = new List<produto>();
            
            foreach (var product in products) {
                if (count == 0) {
                    x = new List<produto>();
                }

                x.Add(product);

                count++;
                total++;

                if (count == 3 || total == products.Count())
                {
                    slideProducts.Add(x);
                    count = 0;
                }
            }

            ViewBag.SlideProducts = slideProducts;           
            
            return View();
        }

        public ActionResult Produtos()
        {           
            var products = MaisLifeModel.DatabaseContext.Model.produto.ToList();
            ViewBag.Products = products;
            Injections.LayoutInjection(this);
            return View();
        }

        public ActionResult Produto(int id)
        {           
            var products = MaisLifeModel.DatabaseContext.Model.produto;
            var product = products.FirstOrDefault(f => f.id == id);
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
                var product = MaisLifeModel.DatabaseContext.Model.produto.FirstOrDefault(f => f.id == id);
                // CHECAMOS DE O PRODUTO PASSADO EXISTE
                if (product != null)
                {
                    usuario user = (usuario)HttpContext.Session["user"];
                    // CHECAMOS DE HÁ ALGUM USUÁRIO LOGADO
                    if (user != null)
                    {
                        carrinho cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                        // CHECAMOS SE HÁ ALGUM CARRINHO ATIVO
                        if (cart == null)
                        {
                            cart = new carrinho()
                            {
                                usuario1 = user,
                                status = "Ativo"
                            };

                            MaisLifeModel.DatabaseContext.Model.carrinho.Add(cart);
                            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                                MaisLifeModel.DatabaseContext.Model.SaveChanges();
                        }
                        
                        carrinho_produto rel = cart.checkProduct(product);
                        // CHECAMOS SE O PRODUTO JÁ ESTÁ NO CARRINHO
                        if (rel == null)
                        {
                            rel = new carrinho_produto()
                            {
                                produto1 = product,
                                carrinho1 = cart,
                                quantidade = 1
                            };
                        }
                        else
                            rel.quantidade++;

                        // SALVA/EDITA RELAÇÃO NO BANCO DE DADOS
                        MaisLifeModel.DatabaseContext.Model.carrinho_produto.Add(rel);
                        //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                            MaisLifeModel.DatabaseContext.Model.SaveChanges();

                    }
                    else
                    {
                        produto produto = new produto(){
                            id = id
                        };

                        Sessions.AddProductInShoppingCart(produto);                        
                    }

                }
            }
            else
            {
                usuario user = (usuario)HttpContext.Session["user"];
                 // CHECAMOS DE HÁ ALGUM USUÁRIO LOGADO
                if (user != null)
                {
                    carrinho cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                    if (cart == null)
                    {
                        cart = new carrinho()
                        {
                            usuario1 = user,
                            status = "Ativo"
                        };

                        MaisLifeModel.DatabaseContext.Model.carrinho.Add(cart);
                        //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                            MaisLifeModel.DatabaseContext.Model.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.Cart = Sessions.FindShoppingCart();
                }
                
            }

            if ( local != 0 )
            {
                ViewBag.Local = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(f => f.id == local); ;
            }

            ViewBag.Locals = MaisLifeModel.DatabaseContext.Model.bairro.ToList();

            Injections.LayoutInjection(this);
            return View();
        }

        public ActionResult AlterarCarrinho()
        {

            var amount = Convert.ToInt32(Request.Form["amount"]);            

            if (amount > 0)
            {
                usuario user = (usuario)HttpContext.Session["user"];
                if (user != null) {
                    carrinho cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                    if (cart != null)
                    {
                        for (var i = 1; i <= amount; i++)
                        {
                            var qtd = Convert.ToInt32(Request.Form["qtd-" + i]);
                            var id = Convert.ToInt32(Request.Form["hidden-" + i]);

                            var rel = cart.carrinho_produto.FirstOrDefault(f => f.id == id);
                            rel.quantidade = qtd;

                            if (rel.quantidade <= 0)
                            {
                                cart.carrinho_produto.Remove(rel);
                                MaisLifeModel.DatabaseContext.Model.carrinho_produto.Remove(rel);
                               // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                                    MaisLifeModel.DatabaseContext.Model.SaveChanges();
                            }
                        }

                        MaisLifeModel.DatabaseContext.Model.carrinho.Add(cart);
                       // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                            MaisLifeModel.DatabaseContext.Model.SaveChanges();

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


            return RedirectToAction("Carrinho", "Home", new { id = 0, local = 0, delivery = 0});
        }

        public ActionResult EnderecoEPagamento() 
        {
            usuario user = (usuario)HttpContext.Session["user"];
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
                endereco address = MaisLifeModel.DatabaseContext.Model.endereco.FirstOrDefault(f => f.id == id);
                if (address != null)
                {
                    usuario user = (usuario)HttpContext.Session["user"];
                    carrinho cart = user.carrinho.FirstOrDefault(f => f.status == "Ativo");
                    

                    var metodo = Request.Form["payMethod"];                                    
                    var payValue = Convert.ToDecimal(Request.Form["payValue"]);
                    if (payValue < cart.Total(address.bairro1.taxa) && metodo == "Dinheiro")
                    {
                        TempData["Error"] = "Valor digitado é menor que o valor total da compra.";
                        return RedirectToAction("EnderecoEPagamento", "Home");
                    }
                    else
                    {
                        pedido order = new pedido()
                        {
                            usuario1 = user,
                            valor = cart.Total(address.bairro1.taxa),
                            carrinho1 = cart,
                            endereco1 = address,
                            pago = payValue,
                            metodo = metodo,
                            status = "Em aberto",
                            data = DateTime.Now,
                            tipo = "Venda",
                            origem = "Site",
                            desconto = 0                            
                        };

                        if (metodo == "Prazo")
                        {
                            var parcels = Convert.ToInt32(Request.Form["payment-parcels"]);
                            if (parcels < 1 || parcels > 3)
                            {
                                TempData["Error"] = "Número de parcelas deve ser entre 1 e 3.";
                                return RedirectToAction("EnderecoEPagamento", "Home");
                            }
                            else {
                                order.parcelas = parcels;
                            }
                        }  

                        order.previsao_entrega = Helper.CalculateShipping.findShippingDate(order);

                        MaisLifeModel.DatabaseContext.Model.pedido.Add(order);

                        cart.status = "Fechado";
                        MaisLifeModel.DatabaseContext.Model.carrinho.Add(cart);

                        carrinho newCart = new carrinho()
                        {
                            usuario1 = user,
                            status = "Ativo"
                        };

                        MaisLifeModel.DatabaseContext.Model.carrinho.Add(newCart);

                        //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                            MaisLifeModel.DatabaseContext.Model.SaveChanges();
                        
                        return RedirectToAction("Perfil", "Home");
                    }

                }
                else
                {
                    TempData["Error"] = "Endereço não informado.";                   
                    return RedirectToAction("EnderecoEPagamento", "Home");
                }
                    


            }catch(Exception){
                TempData["Error"] = "Houve um erro na autenticação.";  
                return RedirectToAction("EnderecoEPagamento", "Home");
            }
            
        }
        
        public ActionResult CalcularEntrega(){

            var localString = Request.Form["local"];
            bairro local = null;

            if (localString != "0" && localString != "")
            {
                local = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(f => f.nome == localString);
            }

            decimal valueDelivery = CalculateShipping.Calculate(local);

            return RedirectToAction("FinalizarPedido", "Home");

        }

        public ActionResult NovoEndereco(EnderecoAdapter endereco)
        {
            bairro bairro = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(f => f.nome.ToLower() == endereco.Bairro.ToLower());
            if (bairro != null)
            {
                usuario user = (usuario)HttpContext.Session["user"];

                if (user != null)
                {
                    endereco.Usuario = user.id;
                    endereco.Pais = "Brasil";
                    endereco.Estado = "MG";

                    var end = endereco.ToEndereco();
                    end.bairro1 = bairro;

                    MaisLifeModel.DatabaseContext.Model.endereco.Add(end);
                    //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                        MaisLifeModel.DatabaseContext.Model.SaveChanges();
                }
                
            }           

            return RedirectToAction("EnderecoEPagamento", "Home");
        }

        public ActionResult CreateContact(ContatoAdapter contato)
        {
            MaisLifeModel.DatabaseContext.Model.contato.Add(contato.ToContato());
            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
            //{
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
            //}
            return RedirectToAction("Index");
        }

        public ActionResult CreateUser(UsuarioAdapter user)
        {
            if (Validation.ExistingEmailValidation(user))
            {
                var newUser = user.ToUsuario();
                newUser.permissao = 0;
                newUser.tipo = "client";
                MaisLifeModel.DatabaseContext.Model.usuario.Add(newUser);
                //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                //{
                    MaisLifeModel.DatabaseContext.Model.SaveChanges();
                    Sessions.CreateCookie(user.ToUsuario(), false);
                //}
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
                carrinho cartCookie = Sessions.FindShoppingCart();

                if (cartCookie != null && cartCookie.carrinho_produto.Count() > 0)
                {
                    //RECUPERA CARRINHO BD
                    carrinho cartBd = usuario.carrinho.FirstOrDefault(f => f.status == "Ativo");
                    List<carrinho_produto> relCartCookie = cartCookie.carrinho_produto.ToList();

                    //PASSANDO DADOS RELAÇÃO BD PARA LISTA AXU
                    List<carrinho_produto> relCartAxu = new List<carrinho_produto>();
                    foreach (carrinho_produto relCokie in relCartCookie)
                    {
                        relCartAxu.Add(relCokie);
                    }
                    //SE EXISTE UM CARRINHO
                    if (cartBd != null)
                    {
                        //LISTA COM RELAÇOES DO BD
                        List<carrinho_produto> relCartBd = cartBd.carrinho_produto.ToList();

                        //PERCORRE AS DUAS LSITAS PROCURANDO POR PRODUTOS IGUAIS
                        foreach (carrinho_produto relacaoCookie in relCartCookie)
                        {
                            foreach (carrinho_produto relacaoBd in relCartBd)
                            {
                                // SE É =
                                if (relacaoCookie.produto1.id == relacaoBd.produto1.id)
                                {
                                    //REMOVE DA LISTA AUXILIAR
                                    relCartAxu.Remove(relacaoCookie);
                                    //MDA JUNTA AS QUANTIDADES
                                    relacaoBd.quantidade += relacaoCookie.quantidade;
                                    
                                    //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                                        MaisLifeModel.DatabaseContext.Model.SaveChanges();
                                    


                                }
                            }
                        }

                        //INSERE COMO NOVO PRODUTO OS QUE NÃO FORAM ENCONTRADOS NA RELAÇÃO EXISTENTE
                        if (relCartAxu.Count > 0)
                        {
                            foreach (carrinho_produto cp in relCartAxu)
                            {
                                cp.carrinho1 = cartBd;

                                MaisLifeModel.DatabaseContext.Model.carrinho_produto.Add(cp);

                                //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                                    MaisLifeModel.DatabaseContext.Model.SaveChanges();
                            }
                        }
                    }
                    //CRIA E INSERE UM CARRINHO CASO NÃO EXISTA
                    else
                    {
                        cartBd = new carrinho()
                        {
                            usuario1 = usuario,
                            status = "Ativo"
                        };

                        MaisLifeModel.DatabaseContext.Model.carrinho.Add(cartBd);

                       // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                            MaisLifeModel.DatabaseContext.Model.SaveChanges();

                        //BUSCA CARRINHO INSERIDO
                        carrinho cartInserted = MaisLifeModel.DatabaseContext.Model.carrinho.FirstOrDefault(f => f.usuario1.id == usuario.id && f.status == "Ativo");

                        //SETA CARRINHO DAS RELAÇÕES E INSERE RELAÇÃO
                        foreach (carrinho_produto cp in relCartCookie)
                        {
                            cp.carrinho1 = cartInserted;

                            MaisLifeModel.DatabaseContext.Model.carrinho_produto.Add(cp);

                           // if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                                MaisLifeModel.DatabaseContext.Model.SaveChanges();
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
            
            usuario user = (usuario)HttpContext.Session["user"];
            if (user == null)
            {
                return RedirectToAction("Index");
            }else{
                ViewBag.User = user;
                return View();
            }

            
        }

        public string AjaxUse_Shipping(int id) { 
            var local = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(f => f.id == id);

            var shippingValue = Helper.CalculateShipping.Calculate(local);

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(shippingValue);
        }       

    }
}