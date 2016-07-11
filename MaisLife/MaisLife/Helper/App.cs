using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class App{

        public static bool IsLogged() { 
            var user = (Usuario)HttpContext.Current.Session["user"];
            if (user == null)
                return false;
            else
                return true;
        }

        public static Usuario Logged() {
            if (IsLogged())
            {
                var logged = (Usuario)HttpContext.Current.Session["user"];
                return ConfigDB.Model.Usuarios.FirstOrDefault(f => f.Id == logged.Id);
            }
            else {
                return null;
            }

            
                
        }

    }
}