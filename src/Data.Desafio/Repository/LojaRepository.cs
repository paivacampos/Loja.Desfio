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

        public override async Task Remover(int id)
        {
            var objRemove = Db.Lojas.AsNoTracking().FirstOrDefault(w => w.Id == id);
            Db.Entry(objRemove).State = EntityState.Deleted;
            Db.Lojas.Remove(objRemove);
            await SaveChanges();
        }
    }
}
