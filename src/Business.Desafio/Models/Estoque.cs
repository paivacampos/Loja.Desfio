using System;

namespace Business.Desafio.Models
{
    public class Estoque : Registro
    {
        public int ProdutoId { get; set; }
        public int LojaId { get; set; }
        public int Quantidade { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public Produto Produto { get; set; }
        public Loja Loja { set; get; }

    }
}