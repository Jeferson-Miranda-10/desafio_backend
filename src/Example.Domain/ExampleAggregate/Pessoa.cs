using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.ExampleAggregate
{
    public sealed class Pessoa
    {
        private Pessoa(string nome, string cPF, int id_Cidade, int idade)
        {
            Nome = nome;
            CPF = cPF;
            Id_Cidade = id_Cidade;
            Idade = idade;

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        [ForeignKey("Pessoa_Cidade")]
        public int Id_Cidade { get; set; }
        public int Idade { get; set; }

        public static Pessoa Create(string nome, string cPF, int id_Cidade, int idade)
        {
            if (nome == null)
                throw new ArgumentException("Invalid " + nameof(nome));

            if (cPF == null)
                throw new ArgumentException("Invalid " + nameof(cPF));

            if (id_Cidade == 0)
                throw new ArgumentException("Invalid " + nameof(id_Cidade));

            if (idade == 0)
                throw new ArgumentException("Invalid " + nameof(idade));


            return new Pessoa(nome, cPF, id_Cidade, idade);
        }

        public void Update(string nome, string cPF, int id_Cidade, int idade)
        {
            if (nome != null)
                Nome = nome;

            if (cPF.Length > 11 || string.IsNullOrEmpty(cPF))
                throw new InvalidAgeExceptions();
            else
                CPF = cPF;

            if (idade != 0)
                Idade = idade;

            if (id_Cidade != 0)
                Id_Cidade = id_Cidade;
        }
    }
}
