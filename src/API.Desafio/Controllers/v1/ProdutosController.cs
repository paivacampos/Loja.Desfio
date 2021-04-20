using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Desafio.DTO;
using AutoMapper;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Desafio.Controllers
{
    [Route("api/v1/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                               IMapper mapper,
                               INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Listagem quado se tem multi produtos, exibe-se todas
        /// </summary>
        /// <returns>Retorna uma JSON com a lista de produtos cadastradas e seus respectivos dados</returns>
        [HttpGet("listarProdutos")]
        public async Task<JsonResult> ListarTodos()
        {
            try
            {
                var objLst = _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
                return new JsonResult(new
                {
                    success = true,
                    data = objLst,
                    mensagem = "",
                    total = objLst.Count()
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível obter esta lista de todos os produtos. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Exibe os detalhes de uma produto pelo seu ID
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns>Retorna os dados do produto ou em branco caso não exista dados ou o ID seja inválido</returns>
        [HttpGet("detalheProduto/{id:int}")]
        public async Task<JsonResult> DetalheId(int id)
        {
            try
            {
                var obj = ObterProdutoId(id);

                return new JsonResult(new
                {
                    success = true,
                    data = obj,
                    mensagem = "",
                    total = 1
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível obter esta lista de lojas. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Adiciona uma loja, passa-se pelo processo de validação, retornando dados que não conferem com os da regra de negócio
        /// </summary>
        /// <param name="loja">Loja a ser adicionada</param>
        /// <returns>Retorna mensagem de confirmação da ação ou erro de algum dado inválido</returns>
        [HttpPost("adicionarProduto")]
        public async Task<JsonResult> Adicionar(ProdutoDto produto)
        {
            try
            {
                if (!ModelState.IsValid) return (JsonResult)CustomResponse(produto);

                await _produtoRepository.Adicionar(_mapper.Map<Produto>(produto));

                return (JsonResult)CustomResponse(produto);
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível adidcionar a loja. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Altera-se a loja, validando o ID passado na URL com o ID que vem dentro de um JSON
        /// </summary>
        /// <param name="id">ID da loja</param>
        /// <param name="loja">Entidade loja a ser alterada</param>
        /// <returns>Retorna mensagem de confirmação da ação ou erro de algum dado inválido</returns>
        [HttpPut("alterarProduto/{id:int}")]
        public async Task<JsonResult> Atualizar(int id, ProdutoDto produto)
        {
            try
            {
                if (id != loja.Id)
                {
                    NotificarErro("O id informado não é o mesmo que foi passado.");
                    return (JsonResult)CustomResponse(produto);
                }

                if (!ModelState.IsValid) return (JsonResult)CustomResponse(ModelState);

                await _produtoRepository.Atualizar(_mapper.Map<Produto>(produto));

                return (JsonResult)CustomResponse(produto);
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível atualiza os dados da loja. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Exclui uma loja
        /// </summary>
        /// <param name="id">ID da loja a ser excluida</param>
        /// <returns>Retorna mensagem de confirmação da ação ou erro de algum dado inválido</returns>
        [HttpDelete("excluirLoja/{id:int}")]
        public async Task<JsonResult> Excluir(int id)
        {
            try
            {
                var objLoja = await ObterProdutoId(id);

                if (objLoja == null) return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi excluir o produto.",
                    total = 0
                });

                await _produtoRepository.Remover(id);

                return new JsonResult(new
                {
                    success = true,
                    data = "",
                    mensagem = "Produto removido com sucesso.",
                    total = 0
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível excluir o produto. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Retorna a entidade Produto
        /// </summary>
        /// <param name="id">Id cadastrado</param>
        /// <returns>Retorna o objeto mapeado de produto</returns>
        private async Task<LojaDto> ObterProdutoId(int id)
        {
            return _mapper.Map<LojaDto>(await _produtoRepository.ObterPorId(id));
        }
    }
}