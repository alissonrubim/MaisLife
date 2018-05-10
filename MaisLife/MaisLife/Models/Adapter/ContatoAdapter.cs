using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class ContatoAdapter
    {
        public int Id { get; set; }
        [Required]
        [StringLength(45,ErrorMessage="Limite de caracteres excedido")]
        public string Nome { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        public string Assunto { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        [DataType(DataType.EmailAddress, ErrorMessage="E-mail inválido")]
        public string Email { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        public string Telefone { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Limite de caracteres excedido")]
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }

        public Contato ToContato()
        {
            return new Contato()
            {
                Id = this.Id,
                Nome = this.Nome,
                Assunto = this.Assunto,
                Email = this.Email,
                Telefone = this.Telefone,
                Mensagem = this.Mensagem
            };
        }

        public ContatoAdapter ToContatoAdapter(Contato contato)
        {
            return new ContatoAdapter()
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Assunto = contato.Assunto,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Mensagem = contato.Mensagem
            };
        }
    }
}