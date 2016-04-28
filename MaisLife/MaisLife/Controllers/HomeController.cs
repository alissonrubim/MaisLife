using MaisLife.Helper;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaisLife.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tezst() 
        {

            return View();
        }

        //---------------------------------------------------------------------------

        public Usuario Login(Usuario user)
        {
            if (user != null)
            {
                Session["user"] = user;
            }
            return user;
        }

        public Usuario CreateCookie(string email, string senha, Boolean keep)
        {
            Usuario user = ConfigDB.Model.Usuarios.Where(f => f.Email == email && f.Senha == senha).First();
            if (user != null)
            {

                HttpCookie cookie = new HttpCookie("UserMaisLife");
                cookie.Value = user.Email;

                //Tempo expiração
                TimeSpan expiration;

                if (keep)
                    expiration = new TimeSpan(365, 0, 0, 0);
                else
                    expiration = new TimeSpan(0, 20, 0);


                cookie.Expires = DateTime.Now + expiration;
                Response.Cookies.Add(cookie);

            }

            return Login(user);
        }

        public void Logout()
        {
            Session.Remove("User");
            Request.Cookies.Remove("UserMaisLife");
        }

        public void AddProductInShoppingCart(Produto produto)
        {
            string[] productsString;
            List<string> productsList = new List<string>();

            //RECUPERA O COOKIE
            HttpCookie cookie = Request.Cookies["shoppingCartMaisLife"];
            

            if (cookie != null)
            {
                bool existingProduct = false;
                //DIVIDE A STRING EM ARRAY
                productsString = cookie.Value.ToString().Split(new Char[] { ',' });

                //ARRAY PARA LISTA PARA MANIPULAÇÃO
                for (int i = 0; i <= productsString.Length - 1; i++)
                {
                    productsList.Add(productsString[i]);
                }
                //AUXILIARES PARA ADD E DELETE DOS PRODUTOS
                string productToDelete = "";
                string productToAdd = "";

                foreach (string p in productsList)
                {
                    //CONVERT PARA INT PARA MANIPULAÇÃO
                    string[] aux = p.Split(new Char[] { ':' });
                    int idProduct = Convert.ToInt16(aux[0]);
                    int amount = Convert.ToInt16(aux[1]);

                    if (idProduct == produto.Id)
                    {
                        amount = (amount + 1);
                        productToDelete = p;
                        productToAdd = idProduct + ":" + amount;
                        existingProduct = true;
                    }
                }

                if (!existingProduct)
                {
                    //ADD NOVO PRODUTO
                    productsList.Add(produto.Id + ":1");
                }
                else
                {
                    //ADD PRODUTO COM NOVA QTD E EXCLUI ANTIGO
                    productsList.Add(productToAdd);
                    productsList.Remove(productToDelete);
                }

            }
            else
            {
                //ADD NOVO PRODUTO
                productsList.Add(produto.Id + ":1");
            }

            //PASSA LISTA PARA STRING
            string productCookie = "";
            int cont = 1;
            foreach (string p in productsList)
            {
                if (!(cont == (productsList.Count)))
                {
                    productCookie += p + ",";
                }
                else
                {
                    productCookie += p;
                }
                cont++;
            }

            //DEFINE COOKIE
            Request.Cookies.Remove("shoppingCartMaisLife");
            cookie = new HttpCookie("shoppingCartMaisLife");
            cookie.Value = productCookie;
            TimeSpan expiration = new TimeSpan(365, 0, 0, 0);
            cookie.Expires = DateTime.Now + expiration;
            Response.Cookies.Add(cookie);

        }

        public void FindShoppingCart()
        {
            Session.Remove("shoppingCar");

            Carrinho cart = new Carrinho();
            List<Carrinho_produto> relProducts = new List<Carrinho_produto>();
            

            string[] productsString;

            HttpCookie cookie = Request.Cookies["shoppingCartMaisLife"];
            productsString = cookie.Value.ToString().Split(new Char[] { ',' });

            for (int i = 0; i <= productsString.Length - 1; i++)
            {
                string[] aux = productsString[i].Split(new Char[] { ':' });
                int idProduct = Convert.ToInt16(aux[0]);
                int amount = Convert.ToInt16(aux[1]);

                Carrinho_produto relProduct = new Carrinho_produto();
                relProduct.Carrinho1 = cart;

                Produto product = ConfigDB.Model.Produtos.Where(f => f.Id == idProduct).First();
                relProduct.Produto1 = product;
                relProduct.Quantidade = amount;

                relProducts.Add(relProduct);                
            }

            cart.Carrinho_produtos = relProducts;
            Session["shoppingCar"] = cart;

        }

    }
}