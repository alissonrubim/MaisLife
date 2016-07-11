using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaisLifeModel;
using MaisLife.Models.Adapter;
using System.IO;
using MaisLife.Helper;

namespace MaisLife.ViewModels.AdminViewModels
{
    public class ParceiroViewModel{

        public HttpRequestBase Request { get; set; }
        public Parceiro Parceiro { get; set; }

        public ParceiroViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public ParceiroViewModel(HttpRequestBase r, ParceiroAdapter adapter)
        {
            this.Request = r;
            this.Parceiro = adapter.ToParceiro();
        }

        public void ParceiroCreateOrEdit() {

            if (this.Parceiro.Id == 0)
            {
                ConfigDB.Model.Add(this.Parceiro);
            }
            else
            {
                var newPatner = this.Parceiro;
                this.Parceiro = ConfigDB.Model.Parceiros.FirstOrDefault(f => f.Id == this.Parceiro.Id);
                this.Parceiro.Nome = newPatner.Nome;
                this.Parceiro.Enderec = newPatner.Enderec;
                this.Parceiro.Telefone = newPatner.Telefone;
                this.Parceiro.Site = newPatner.Site;
                this.Parceiro.Facebook = newPatner.Facebook;
                this.Parceiro.Imagem = newPatner.Imagem;
                ConfigDB.Model.Add(this.Parceiro);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
        }

        public void DoRemove() {

            var fr = new FastRequest(this.Request);

            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var patner = ConfigDB.Model.Parceiros.FirstOrDefault(p => p.Id == id);
                ConfigDB.Model.Delete(patner);
            }

            if (ConfigDB.Model.HasChanges)
                ConfigDB.Model.SaveChanges();
        }

        public int DoEdit() {
            var fr = new FastRequest(this.Request);
            return fr.ToInt("item");
        }

    }
}