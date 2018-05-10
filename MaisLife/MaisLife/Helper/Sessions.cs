using MaisLife.Helper;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MaisLife.Helper
{
    public class Sessions
    {
        private static usuario Login(usuario user)
        {
            if (user != null)
            {
                HttpContext.Current.Session["user"] = MaisLifeModel.DatabaseContext.Model.usuario.Where(f => f.email == user.email).FirstOrDefault();
            }
            return user;
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Remove("User");
            HttpContext.Current.Request.Cookies.Remove("UserMaisLife");
        }

        public static usuario CreateCookie(usuario user, Boolean keep)
        {
            if (user != null)
            {

                HttpCookie cookie = new HttpCookie("UserMaisLife");
                cookie.Value = user.email;

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

        public static void AddProductInShoppingCart(produto produto)
        {
            string[] productsString;
            List<string> productsList = new List<string>();

            //RECUPERA O COOKIE
            HttpCookie cookie = HttpContext.Current.Request.Cookies["shoppingCartMaisLife"];


            if (cookie != null && cookie.Value != "")
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

                    if (idProduct == produto.id)
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
                    productsList.Add(produto.id + ":1");
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
                productsList.Add(produto.id + ":1");
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

            //REMOVE E DEFINE NOVO COOKIE
            HttpContext.Current.Request.Cookies.Remove("shoppingCartMaisLife");
            
            cookie = new HttpCookie("shoppingCartMaisLife");
            cookie.Value = productCookie;
            TimeSpan expiration = new TimeSpan(365, 0, 0, 0);
            cookie.Expires = DateTime.Now + expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static void LimparCookie()
        {
            HttpContext.Current.Response.Cookies["shoppingCartMaisLife"].Expires = DateTime.Now.AddDays(-1);
        }

        public static MaisLifeModel.Models.carrinho FindShoppingCart()
        {

            List<carrinho_produto> relProducts = new List<carrinho_produto>();

            string[] productsString;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["shoppingCartMaisLife"];
            if(cookie != null && cookie.Value != "")
            {
               productsString = cookie.Value.ToString().Split(new Char[] { ',' });

               for (int i = 0; i <= productsString.Length - 1; i++)
               {
                   string[] aux = productsString[i].Split(new Char[] { ':' });
                   int idProduct = Convert.ToInt16(aux[0]);
                   int amount = Convert.ToInt16(aux[1]);

                   carrinho_produto relProduct = new carrinho_produto();

                   produto product = MaisLifeModel.DatabaseContext.Model.produto.Where(f => f.id == idProduct).First();
                   relProduct.produto1 = product;
                   relProduct.quantidade = amount;

                   relProducts.Add(relProduct);
               }
            }

            carrinho cart = new carrinho()
            {
                carrinho_produto = relProducts,
                status = "Ativo"
            };

            return cart;

        }

        public static void EditProductInShoppingCart(int id, int amount)
        {
            string[] productsString;
            List<string> productsList = new List<string>();

            HttpCookie cookie = HttpContext.Current.Request.Cookies["shoppingCartMaisLife"];


            if (cookie != null)
            {
                productsString = cookie.Value.ToString().Split(new Char[] { ',' });

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

                    if (idProduct == id)
                    {
                        productToDelete = p;
                        productToAdd = idProduct + ":" + amount;
                    }
                }

                if(amount > 0)
                {
                    productsList.Add(productToAdd);
                    productsList.Remove(productToDelete);
                }
                else
                {
                    productsList.Remove(productToDelete);
                }
                


            }

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

            HttpContext.Current.Request.Cookies.Remove("shoppingCartMaisLife");

                cookie = new HttpCookie("shoppingCartMaisLife");
                cookie.Value = productCookie;
                TimeSpan expiration = new TimeSpan(365, 0, 0, 0);
                cookie.Expires = DateTime.Now + expiration;
                HttpContext.Current.Response.Cookies.Add(cookie);
            

        }
    }
}