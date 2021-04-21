using System.Threading.Tasks;
using Business.Desafio.Models;

namespace Business.Desafio.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        public Task<bool> ProdutoNomeJaExiste(string nome);
    }
}