using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaisLifeModel;
using MaisLife.Models.Adapter;
using System.IO;
using MaisLife.Helper;
using MaisLifeModel.Models;

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
                MaisLifeModel.DatabaseContext.Model.Parceiro.Add(this.Parceiro);
            }
            else
            {
                var newPatner = this.Parceiro;
                this.Parceiro = MaisLifeModel.DatabaseContext.Model.Parceiro.FirstOrDefault(f => f.Id == this.Parceiro.Id);
                this.Parceiro.Nome = newPatner.Nome;
                this.Parceiro.Enderec = newPatner.Enderec;
                this.Parceiro.Telefone = newPatner.Telefone;
                this.Parceiro.Site = newPatner.Site;
                this.Parceiro.Facebook = newPatner.Facebook;
                this.Parceiro.Imagem = newPatner.Imagem;
                MaisLifeModel.DatabaseContext.Model.Parceiro.Add(this.Parceiro);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public void DoRemove() {

            var fr = new FastRequest(this.Request);

            var count = fr.ToInt("count");
            for (var i = 1; i <= count; i++)
            {
                var id = fr.ToInt("item-" + i);
                var patner = MaisLifeModel.DatabaseContext.Model.Parceiro.FirstOrDefault(p => p.Id == id);
                MaisLifeModel.DatabaseContext.Model.Parceiro.Remove(patner);
            }

            //if (MaisLifeModel.DatabaseContext.Model.HasChanges)
                MaisLifeModel.DatabaseContext.Model.SaveChanges();
        }

        public int DoEdit() {
            var fr = new FastRequest(this.Request);
            return fr.ToInt("item");
        }

    }
}