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

        public endereco ToEndereco()
        {            
            return new endereco(){
                id = this.Id,
                usuario = this.Usuario,
                pais = this.Pais,
                estado = this.Estado,
                cidade = this.Cidade,
                bairro1 = MaisLifeModel.DatabaseContext.Model.bairro.FirstOrDefault(f => f.nome == this.Bairro),
                rua = this.Rua,
                numero = this.Numero,
                cep = this.Cep
            };
        }

        public EnderecoAdapter ToEnderecoAdapter(endereco endereco)
        {
            return new EnderecoAdapter()
            {
                Id = endereco.id,
                Usuario = (int) endereco.usuario,
                Pais = endereco.pais,
                Estado = endereco.estado,
                Cidade = endereco.cidade,
                Bairro = endereco.bairro1.nome,
                Rua = endereco.rua,
                Numero = endereco.numero,
                Cep = endereco.cep
            };
        }



    }
}