using MaisLife.Helper;
using MaisLife.Models.Adapter;
using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaisLife.ViewModels.AdminViewModels
{
    public class UsuarioViewModel{

        public HttpRequestBase Request { get; set; }
        public Usuario Usuario { get; set; }

        public UsuarioViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public UsuarioViewModel(HttpRequestBase r, UsuarioAdapter adapter)
        {
            this.Request = r;
            this.Usuario = adapter.ToUsuario();
        }

        public void UsuarioCreateOrEdit() {

            if (this.Usuario.Id > 0) {

                var newUser = this.Usuario;
                this.Usuario = MaisLifeModel.DatabaseContext.Model.Usuario.FirstOrDefault(f => f.Id == this.Usuario.Id);

                this.Usuario.Nome = newUser.Nome;
                this.Usuario.Sobrenome = newUser.Sobrenome;
                this.Usuario.Email = newUser.Email;
                this.Usuario.Senha = newUser.Senha;
                this.Usuario.Permissao = newUser.Permissao;

            }

            if (this.Usuario.Permissao == 0)
            {
                this.Usuario.Tipo = "Cliente";
            }
            else if (this.Usuario.Permissao == 1)
            {
                this.Usuario.Tipo = "Vendedor";
            }
            else if (this.Usuario.Permissao == 2)
            {
                this.Usuario.Tipo = "Administrador";
            }

            MaisLifeModel.DatabaseContext.Model.Usuario.Add(this.Usuario);

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();

        }

        public void DoRemove()
        {
            var fr = new FastRequest(this.Request);

            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var user = MaisLifeModel.DatabaseContext.Model.Usuario.FirstOrDefault(u => u.Id == id);
                MaisLifeModel.DatabaseContext.Model.Usuario.Remove(user);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public int DoEdit()
        {
            var fr = new FastRequest(this.Request);
            return fr.ToInt("item");
        }

    }
}