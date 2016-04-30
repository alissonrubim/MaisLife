using MaisLife.Helper;
using MaisLife.Models.Adapter;
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
            if (Validation.ExistingEmailValidation(user.Email))
            {
                ConfigDB.Model.Add(user.toUsuario());
                if (ConfigDB.Model.HasChanges)
                {
                    ConfigDB.Model.SaveChanges();

                    Sessions.CreateCookie(user.toUsuario(), false);
                }
            }
            else
            {
                TempData["MessageErroRegister"] = "E-mail ja cadastrado";
            }

            return RedirectToAction("Index");
        }

    }
}