using MaisLife.Models.Adapter;
using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class Validation
    {
        public static bool ValidationLogin(string email, string senha)
        {
            var user = ConfigDB.Model.Usuarios.Where(f => f.Email == email && f.Senha == senha).FirstOrDefault();

            if (user != null)
                return true;

            return false;
        }

        public static bool ExistingEmailValidation(string email)
        {
            var user = ConfigDB.Model.Usuarios.Where(f => f.Email == email).FirstOrDefault();

            if (user == null)
                return true;

            return false;
        }
    }
}