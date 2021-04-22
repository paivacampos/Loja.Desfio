using System;
using System.Threading.Tasks;
using Business.Desafio.Models;
using Loja = Business.Desafio.Service.LojaService;

namespace Business.Desafio.Interfaces
{
    public interface IEstoqueService : IDisposable
    {
        Task Adicionar(Estoque estoque);
        Task Atualizar(Estoque estoque);
        Task Remover(int id);
    }
}