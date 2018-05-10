using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teste.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
  

            ViewBag.bairro =  MaisLifeModel.DatabaseContext.Model.bairro.ToList();

            return View();
        }
    }
}