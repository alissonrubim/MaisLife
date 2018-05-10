using MaisLifeModel;
using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaisLife.Models.Adapter
{
    public class EnderecoAdapter{

        public int Id { get; set; }
        public int Usuario { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Limite de caracteres excedido")]
        public string Cidade { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Limite de caracteres excedido")]
        public string Bairro { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Limite de caracteres excedido")]
        public string Rua { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Limite de caracteres excedido")]
        public string Numero { get; set; }        
        public string Cep { get; set; }

        public Endereco ToEndereco()
        {            
            return new Endereco(){
                Id = this.Id,
                Usuario = this.Usuario,
                Pais = this.Pais,
                Estado = this.Estado,
                Cidade = this.Cidade,
                Bairro1 = MaisLifeModel.DatabaseContext.Model.Bairro.FirstOrDefault(f => f.Nome == this.Bairro),
                Rua = this.Rua,
                Numero = this.Numero,
                Cep = this.Cep
            };
        }

        public EnderecoAdapter ToEnderecoAdapter(Endereco endereco)
        {
            return new EnderecoAdapter()
            {
                Id = endereco.Id,
                Usuario = (int) endereco.Usuario,
                Pais = endereco.Pais,
                Estado = endereco.Estado,
                Cidade = endereco.Cidade,
                Bairro = endereco.Bairro1.Nome,
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Cep = endereco.Cep
            };
        }



    }
}