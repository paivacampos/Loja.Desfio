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

        public ProdutoService(INotificador notificador, IProdutoRepository produtoRepository) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
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

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            if (_produtoRepository.Buscar(f => f.Nome.Equals(produto.Nome) && f.Id != produto.Id).Result.Any())
            {
                Notificar("Já existe um Produto informado com este nome informado.");
                return;
            }

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(int id)
        {
            if (_produtoRepository.ObterPorId(id).Result.EstoqueList.Any())
            {
                Notificar("o Produto possui itens de estoque cadastrados cadastrados!");
                return;
            }

            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}