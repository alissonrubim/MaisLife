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

        public contato ToContato()
        {
            return new contato()
            {
                id = this.Id,
                nome = this.Nome,
                assunto = this.Assunto,
                email = this.Email,
                telefone = this.Telefone,
                mensagem = this.Mensagem
            };
        }

        public ContatoAdapter ToContatoAdapter(contato contato)
        {
            return new ContatoAdapter()
            {
                Id = contato.id,
                Nome = contato.nome,
                Assunto = contato.assunto,
                Email = contato.email,
                Telefone = contato.telefone,
                Mensagem = contato.mensagem
            };
        }
    }
}