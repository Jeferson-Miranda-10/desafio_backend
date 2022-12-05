using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Example.Domain.ExampleAggregate
{
    public sealed class Cidade
    {
        public Cidade() { }
        private Cidade(string nome, string uf)
        {
            Nome = nome;
            UF = uf;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public static Cidade Create(string nome, string uf)
        {
            if (nome == null)
                throw new ArgumentException("Invalid " + nameof(nome));

            if (uf == null)
                throw new ArgumentException("Invalid " + nameof(uf));


            return new Cidade(nome, uf);
        }

        public void Update(string nome, string uf)
        {
            if (nome != null)
                Nome = nome;

            if (uf == null)
                throw new InvalidAgeExceptions();
            else
                UF = uf;
        }
    }
}
