using System;
using System.Threading.Tasks;
using Business.Desafio.Models;

namespace Business.Desafio.Interfaces
{
    public interface ILojaRepository : IRepository<Loja>
    {
        Task<Estoque> ObterFornecedorProdutosEndereco(int id);
    }
}