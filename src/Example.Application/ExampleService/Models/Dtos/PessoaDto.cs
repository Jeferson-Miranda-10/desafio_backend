using Example.Domain.ExampleAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.ExampleService.Models.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Id_Cidade { get; set; }
        public int Idade { get; set; }


        public static explicit operator PessoaDto(Domain.ExampleAggregate.Pessoa v)
        {
            return new PessoaDto()
            {
                Id = v.Id,
                Nome = v.Nome,
                CPF = v.CPF,
                Id_Cidade= v.Id_Cidade,
                Idade = v.Idade,
               
                
            };
        }

    }
}
