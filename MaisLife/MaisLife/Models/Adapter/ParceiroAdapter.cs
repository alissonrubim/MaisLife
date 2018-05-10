using MaisLifeModel;
using MaisLifeModel.Models;
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
        [Required]
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Telefone { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Site { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Facebook { get; set; }
        [StringLength(255, ErrorMessage = "Limite de caracteres excedido")]
        public string Imagem { get; set; }

        public parceiro ToParceiro()
        {
            return new parceiro()
            {               
                id = this.Id,
                nome = this.Nome,
                enderec = this.Endereco,
                telefone = this.Telefone,
                site = this.Site,
                facebook = this.Facebook,
                imagem = this.Imagem
            };
        }

        public ParceiroAdapter ToParceiroAdapter(parceiro patner)
        {
            return new ParceiroAdapter()
            {
                Id = patner.id,
                Nome = patner.nome,
                Endereco = patner.enderec,
                Telefone = patner.telefone,
                Site = patner.site,
                Facebook = patner.facebook,
                Imagem = patner.imagem
            };
        }

    }
}