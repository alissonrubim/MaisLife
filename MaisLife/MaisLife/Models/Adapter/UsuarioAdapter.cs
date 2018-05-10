using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class UsuarioAdapter{

        public int Id { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        public string Nome { get; set; }
        [Required]
        [StringLength(45, ErrorMessage = "Limite de caracteres excedido")]
        public string Sobrenome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required]            
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve conter de 6 a 10 dígitos")]  
        public string Senha { get; set; }
        public int Permissao { get; set; }     

        public Usuario ToUsuario()
        {
            return new Usuario()
            {
                Id = this.Id,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                Email = this.Email,
                Senha = this.Senha,
                Permissao = this.Permissao          
            };
        }

        public UsuarioAdapter ToUsuarioAdapter(Usuario usuario)
        {
            return new UsuarioAdapter()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Permissao = (int)usuario.Permissao                           
            };
        }

    }
}