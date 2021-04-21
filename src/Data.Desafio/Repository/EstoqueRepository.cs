using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Data.Desafio.Context;

namespace Data.Desafio.Repository
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(DbAPIContext context) : base(context) { }

        
    }
}