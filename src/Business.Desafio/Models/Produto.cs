using System.Collections.Generic;

namespace Business.Desafio.Models
{
    public class Produto : Registro
    {
        public string Nome { get; set; }
        public decimal ValorCompra { get; set; }

        public IEnumerable<Estoque> EstoqueList { get; set; }
    }
}