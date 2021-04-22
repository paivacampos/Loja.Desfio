using System;
using System.Threading.Tasks;
using Business.Desafio.Models;
using Business.Desafio.Service;

namespace Business.Desafio.Interfaces
{
    public interface ILojaService : IDisposable
    {
        Task Adicionar(Loja loja);
        Task Atualizar(Loja loja);
        Task Remover(int id);
    }
}