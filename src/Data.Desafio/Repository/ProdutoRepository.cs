using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Data.Desafio.Context;

namespace Data.Desafio.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DbAPIContext context) : base(context) { }
    }
}