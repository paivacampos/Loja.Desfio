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
    [Route("api/v1/estoque")]
    public class EstoqueController :MainController
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMapper _mapper;

        public EstoqueController(IEstoqueRepository estoqueRepository, 
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _estoqueRepository = estoqueRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Exibe os detalhes de uma produto pelo seu ID
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns>Retorna os dados do produto ou em branco caso não exista dados ou o ID seja inválido</returns>
        [HttpGet("detalheEstoqueProduto/{id:int}")]
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
        public async Task<JsonResult> Adicionar(EstoqueDto estoque)
        {
            try
            {
                if (!ModelState.IsValid) return (JsonResult)CustomResponse(ModelState);

                await _estoqueRepository.Adicionar(_mapper.Map<Estoque>(estoque));

                return (JsonResult)CustomResponse(estoque);
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível adidcionar o estoque do produto na loja. Tente novamente depois.",
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
        [HttpPut("alterarEstoqueProduto/{id:int}")]
        public async Task<JsonResult> Atualizar(int id, EstoqueDto estoque)
        {
            try
            {
                if (id != estoque.Id)
                {
                    NotificarErro("O id informado não é o mesmo que foi passado.");
                    return (JsonResult)CustomResponse(estoque);
                }

                if (!ModelState.IsValid) return (JsonResult)CustomResponse(ModelState);

                await _estoqueRepository.Atualizar(_mapper.Map<Estoque>(estoque));

                return (JsonResult)CustomResponse(ModelState);
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
        /// Exclui umestoque de produto
        /// </summary>
        /// <param name="id">ID do estoque</param>
        /// <returns>Retorna mensagem de confirmação da ação ou erro de algum dado inválido</returns>
        [HttpDelete("excluirEstoqueProduto/{id:int}")]
        public async Task<JsonResult> Excluir(int id)
        {
            try
            {
                var objEstoque = await ObterProdutoId(id);

                if (objEstoque == null) return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi excluir o estoque do produto.",
                    total = 0
                });

                await _estoqueRepository.Remover(id);

                return new JsonResult(new
                {
                    success = true,
                    data = "",
                    mensagem = "Estoque do produto removido com sucesso.",
                    total = 0
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível excluir o estoque do produto. Tente novamente depois.",
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
            return _mapper.Map<LojaDto>(await _estoqueRepository.ObterPorId(id));
        }
    }
}