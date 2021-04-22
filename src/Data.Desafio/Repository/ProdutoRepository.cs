using System.Threading.Tasks;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Data.Desafio.Context;
using System.Linq;

namespace Data.Desafio.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DbAPIContext context) : base(context) { }
        public async Task<bool> ProdutoNomeJaExiste(string nome)
        {
            return (Db.Produtos.FirstOrDefault(w => w.Nome.Equals(nome)) == null) ? true : false;
        }
        
        public override async Task Remover(int id)
        {
            var objRemove = Db.Produtos.AsNoTracking().FirstOrDefault(w => w.Id == id);
            Db.Entry(objRemove).State = EntityState.Deleted;
            Db.Produtos.Remove(objRemove);
            await SaveChanges();
        }
    }
}
