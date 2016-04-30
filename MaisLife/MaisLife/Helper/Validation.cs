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
        public static bool ValidationLogin(UsuarioAdapter u)
        {
            var user = ConfigDB.Model.Usuarios.Where(f => f.Email == u.Email && f.Senha == u.Senha).FirstOrDefault();

            if (user != null)
                return true;

            return false;
        }

        public static bool ExistingEmailValidation(UsuarioAdapter u)
        {
            var user = ConfigDB.Model.Usuarios.Where(f => f.Email == u.Email).FirstOrDefault();

            if (user == null)
                return true;

            return false;
        }
    }
}