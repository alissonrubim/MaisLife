using MaisLife.Models.Adapter;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.Helper
{
    public class Validation
    {
        public static usuario ValidationLogin(UsuarioAdapter u)
        {
            var user = MaisLifeModel.DatabaseContext.Model.usuario.Where(f => f.email == u.Email && f.senha == u.Senha).FirstOrDefault();

            return user;
        }

        public static bool ExistingEmailValidation(UsuarioAdapter u)
        {
            var user = MaisLifeModel.DatabaseContext.Model.usuario.Where(f => f.email == u.Email).FirstOrDefault();

            if (user == null)
                return true;

            return false;
        }
    }
}