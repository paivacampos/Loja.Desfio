using System;
using System.Linq;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using System.Threading.Tasks;
using Business.Desafio.Models.Validations;

namespace Business.Desafio.Service
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueRepository _estoqueRepository;

        public ProdutoService(INotificador notificador,
                              IProdutoRepository produtoRepository,
                              IEstoqueRepository estoqueRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _estoqueRepository = estoqueRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            try
            {
                if (ExecutarValidacao(new ProdutoValidation(), produto))
                {
                    if (_produtoRepository.Buscar(f => f.Nome.Equals(produto.Nome)).Result.Any())
                    {
                        Notificar("Já existe um Produto informado com este nome.");
                        return;
                    }

                    await _produtoRepository.Adicionar(produto);
                }
            }
            catch (Exception e)
            {
                Notificar("Não foi possível realizar a operação no momento.");
            }
        }

        public async Task Atualizar(Produto produto)
        {
            try
            {
                if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

                if (_produtoRepository.Buscar(f => f.Nome.Equals(produto.Nome) && f.Id != produto.Id).Result.Any())
                {
                    Notificar("Já existe um Produto informado com este nome informado.");
                    return;
                }

                await _produtoRepository.Atualizar(produto);
            }
            catch (Exception e)
            {
                Notificar("Não foi possível realizar a operação no momento.");
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var produtoEstoque = await _estoqueRepository.ListarEstoqueProduto(id);
                if (produtoEstoque != null)
                {
                    Notificar("o Produto possui itens de estoque cadastrados cadastrados!");
                    return;
                }

                await _produtoRepository.Remover(id);
            }
            catch (Exception e)
            {
                Notificar("Não foi possível realizar a operação no momento.");
            }
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}