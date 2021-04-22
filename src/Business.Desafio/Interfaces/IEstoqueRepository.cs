using System.Collections.Generic;
using Business.Desafio.Models;
using System.Threading.Tasks;

namespace Business.Desafio.Interfaces
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        Task<List<Estoque>> ListarEstoqueLoja(int id);

        Task<Estoque> ListarEstoqueProduto(int id);
    }
}