using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Data.Desafio.Context;

namespace Data.Desafio.Repository
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        public LojaRepository(DbAPIContext context) : base(context) { }
        
    }
}