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

        public usuario ToUsuario()
        {
            return new usuario()
            {
                id = this.Id,
                nome = this.Nome,
                sobrenome = this.Sobrenome,
                email = this.Email,
                senha = this.Senha,
                permissao = this.Permissao          
            };
        }

        public UsuarioAdapter ToUsuarioAdapter(usuario usuario)
        {
            return new UsuarioAdapter()
            {
                Id = usuario.id,
                Nome = usuario.nome,
                Sobrenome = usuario.sobrenome,
                Email = usuario.email,
                Senha = usuario.senha,
                Permissao = (int)usuario.permissao                           
            };
        }

    }
}