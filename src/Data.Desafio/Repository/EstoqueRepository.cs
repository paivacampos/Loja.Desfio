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


        public async Task<List<Estoque>> ListarEstoqueLoja(int id)
        {
            return Db.Estoques.Where(w => w.LojaId == id).ToList();
        }

        public async Task<Estoque> ListarEstoqueProduto(int id)
        {
            return Db.Estoques.FirstOrDefault(w => w.ProdutoId == id);
        }
        
        public override async Task Remover(int id)
        {
            var objRemove = Db.Estoques.AsNoTracking().FirstOrDefault(w => w.Id == id);
            Db.Entry(objRemove).State = EntityState.Deleted;
            Db.Estoques.Remove(objRemove);
            await SaveChanges();
        }
    }
}
