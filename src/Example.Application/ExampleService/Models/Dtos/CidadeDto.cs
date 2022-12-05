﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Example.Application.ExampleService.Models.Dtos
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public static explicit operator CidadeDto(Domain.ExampleAggregate.Cidade v)
        {
            return new CidadeDto()
            {
                Id = v.Id,
                Nome = v.Nome,
                UF = v.UF
            };
        }

    }
}
