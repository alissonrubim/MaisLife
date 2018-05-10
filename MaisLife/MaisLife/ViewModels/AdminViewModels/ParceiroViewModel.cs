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
        public parceiro Parceiro { get; set; }

        public ParceiroViewModel(HttpRequestBase r) {
            this.Request = r;
        }

        public ParceiroViewModel(HttpRequestBase r, ParceiroAdapter adapter)
        {
            this.Request = r;
            this.Parceiro = adapter.ToParceiro();
        }

        public void ParceiroCreateOrEdit() {

            if (this.Parceiro.id == 0)
            {
                MaisLifeModel.DatabaseContext.Model.parceiro.Add(this.Parceiro);
            }
            else
            {
                var newPatner = this.Parceiro;
                this.Parceiro = MaisLifeModel.DatabaseContext.Model.parceiro.FirstOrDefault(f => f.id == this.Parceiro.id);
                this.Parceiro.nome = newPatner.nome;
                this.Parceiro.enderec = newPatner.enderec;
                this.Parceiro.telefone = newPatner.telefone;
                this.Parceiro.site = newPatner.site;
                this.Parceiro.facebook = newPatner.facebook;
                this.Parceiro.imagem = newPatner.imagem;
                MaisLifeModel.DatabaseContext.Model.parceiro.Add(this.Parceiro);
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
                var patner = MaisLifeModel.DatabaseContext.Model.parceiro.FirstOrDefault(p => p.id == id);
                MaisLifeModel.DatabaseContext.Model.parceiro.Remove(patner);
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