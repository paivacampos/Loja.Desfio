using System.Linq;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Data.Desafio.Context;
using System.Threading.Tasks;

namespace Data.Desafio.Repository
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        public LojaRepository(DbAPIContext context) : base(context) { }

        public async Task<bool> LojaCnpjJaExiste(string cnpj)
        {
            return (Db.Lojas.FirstOrDefault(w => w.Cnpj.Equals(cnpj)) == null) ? true : false;
        }
    }
}