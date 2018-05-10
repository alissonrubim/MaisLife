using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class App{

        public static bool IsLogged() { 
            var user = (usuario)HttpContext.Current.Session["user"];
            if (user == null)
                return false;
            else
                return true;
        }

        public static usuario Logged() {
            if (IsLogged())
            {
                var logged = (usuario)HttpContext.Current.Session["user"];
                return MaisLifeModel.DatabaseContext.Model.usuario.FirstOrDefault(f => f.id == logged.id);
            }
            else {
                return null;
            }

            
                
        }

    }
}