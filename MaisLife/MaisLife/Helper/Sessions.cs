using MaisLife.Helper;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MaisLife.Helper
{
    public class Sessions
    {
        private static Usuario Login(Usuario user)
        {
            if (user != null)
            {
                HttpContext.Current.Session["user"] = user;
            }
            return user;
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Remove("User");
            HttpContext.Current.Request.Cookies.Remove("UserMaisLife");
        }

        public static Usuario CreateCookie(Usuario user, Boolean keep)
        {
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
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return Login(user);
        }

        public static void AddProductInShoppingCart(Produto produto)
        {
            string[] productsString;
            List<string> productsList = new List<string>();

            //RECUPERA O COOKIE
            HttpCookie cookie = HttpContext.Current.Request.Cookies["shoppingCartMaisLife"];


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
            HttpContext.Current.Request.Cookies.Remove("shoppingCartMaisLife");
            cookie = new HttpCookie("shoppingCartMaisLife");
            cookie.Value = productCookie;
            TimeSpan expiration = new TimeSpan(365, 0, 0, 0);
            cookie.Expires = DateTime.Now + expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static void FindShoppingCart()
        {
            HttpContext.Current.Session.Remove("shoppingCar");

            Carrinho cart = new Carrinho();
            List<Carrinho_produto> relProducts = new List<Carrinho_produto>();


            string[] productsString;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["shoppingCartMaisLife"];
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
            HttpContext.Current.Session["shoppingCar"] = cart;

        }
    }
}