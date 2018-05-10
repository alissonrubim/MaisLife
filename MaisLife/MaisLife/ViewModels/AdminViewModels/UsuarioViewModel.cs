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
        public usuario Usuario { get; set; }

        public UsuarioViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public UsuarioViewModel(HttpRequestBase r, UsuarioAdapter adapter)
        {
            this.Request = r;
            this.Usuario = adapter.ToUsuario();
        }

        public void UsuarioCreateOrEdit() {

            if (this.Usuario.id > 0) {

                var newUser = this.Usuario;
                this.Usuario = MaisLifeModel.DatabaseContext.Model.usuario.FirstOrDefault(f => f.id == this.Usuario.id);

                this.Usuario.nome = newUser.nome;
                this.Usuario.sobrenome = newUser.sobrenome;
                this.Usuario.email = newUser.email;
                this.Usuario.senha = newUser.senha;
                this.Usuario.permissao = newUser.permissao;

            }

            if (this.Usuario.permissao == 0)
            {
                this.Usuario.tipo = "Cliente";
            }
            else if (this.Usuario.permissao == 1)
            {
                this.Usuario.tipo = "Vendedor";
            }
            else if (this.Usuario.permissao == 2)
            {
                this.Usuario.tipo = "Administrador";
            }

            MaisLifeModel.DatabaseContext.Model.usuario.Add(this.Usuario);

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
                var user = MaisLifeModel.DatabaseContext.Model.usuario.FirstOrDefault(u => u.id == id);
                MaisLifeModel.DatabaseContext.Model.usuario.Remove(user);
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