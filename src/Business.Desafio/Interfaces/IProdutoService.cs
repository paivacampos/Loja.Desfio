using System;
using System.Threading.Tasks;
using Business.Desafio.Models;
using Loja = Business.Desafio.Service.LojaService;

namespace Business.Desafio.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(int id);
    }
}