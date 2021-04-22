using System.Collections.Generic;

namespace Business.Desafio.Models
{
    public class Loja : Registro
    {
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

        public IEnumerable<Estoque> EstoqueList { get; set; }
    }
}