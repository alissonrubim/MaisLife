using MaisLifeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class ParceiroAdapter{

        public int Id { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Limite de caracteres excedido")]
        public string Nome { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Endereco { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Telefone { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Site { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Facebook { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Imagem { get; set; }

        public Parceiro ToParceiro()
        {
            return new Parceiro()
            {               
                Id = this.Id,
                Nome = this.Nome,
                Enderec = this.Endereco,
                Telefone = this.Telefone,
                Site = this.Site,
                Facebook = this.Facebook,
                Imagem = this.Imagem
            };
        }

        public ParceiroAdapter ToParceiroAdapter(Parceiro patner)
        {
            return new ParceiroAdapter()
            {
                Id = patner.Id,
                Nome = patner.Nome,
                Endereco = patner.Enderec,
                Telefone = patner.Telefone,
                Site = patner.Site,
                Facebook = patner.Facebook,
                Imagem = patner.Imagem
            };
        }

    }
}