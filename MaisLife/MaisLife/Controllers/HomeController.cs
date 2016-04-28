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

        public ActionResult Tezst() {

            return View();
        }

        //--------------------------------------------------------------------

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

    }
}