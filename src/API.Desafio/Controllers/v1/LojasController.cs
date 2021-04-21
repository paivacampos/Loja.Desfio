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
    [Route("api/v1/lojas")]
    public class LojasController : MainController
    {
        private readonly ILojaRepository _LojaRepository;
        private readonly ILojaService _lojaService;
        private readonly IMapper _mapper;

        public LojasController(ILojaRepository lojaRepository,
                               IMapper mapper,
                               INotificador notificador, ILojaService lojaService) : base(notificador)
        {
            _LojaRepository = lojaRepository;
            _mapper = mapper;
            _lojaService = lojaService;
        }

        /// <summary>
        /// Exibe os detalhes de uma loja pelo seu ID
        /// </summary>
        /// <param name="id">ID da loja</param>
        /// <returns>Retorna os dados da loja ou em branco caso não exista dados ou o ID seja inválido</returns>
        [HttpGet("detalheLoja/{id:int}")]
        public async Task<JsonResult> DetalheLojasId(int id)
        {
            try
            {
                var obj = ObterLojaId(id);

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
        [HttpPost("adicionarLoja")]
        public async Task<JsonResult> AdicionarLojas(LojaDto loja)
        {
            try
            {
                if (!ModelState.IsValid) return (JsonResult)CustomResponse(loja);

                await _lojaService.Adicionar(_mapper.Map<Loja>(loja));

                return (JsonResult)CustomResponse(loja);
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
        [HttpPut("alterarLoja/{id:int}")]
        public async Task<JsonResult> AtualizarLoja(int id, LojaDto loja)
        {
            try
            {
                if (id != loja.Id)
                {
                    NotificarErro("O id informado não é o mesmo que foi passado.");
                    return (JsonResult)CustomResponse(loja);
                }

                if (!ModelState.IsValid) return (JsonResult)CustomResponse(ModelState);

                await _LojaRepository.Atualizar(_mapper.Map<Loja>(loja));

                return (JsonResult)CustomResponse(loja);
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
                var objLoja = await ObterLojaId(id);

                if (objLoja == null) return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi excluir a loja.",
                    total = 0
                });

                await _LojaRepository.Remover(id);

                return new JsonResult(new
                {
                    success = true,
                    data = "",
                    mensagem = "Loja removida com sucesso.",
                    total = 0
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    data = "",
                    mensagem = "Não foi possível excluir a loja. Tente novamente depois.",
                    total = 0
                });
            }
        }

        /// <summary>
        /// Retorna a entidade Loja
        /// </summary>
        /// <param name="id">Id cadastrado</param>
        /// <returns>Retorna o objeto mapeado de loja</returns>
        private async Task<LojaDto> ObterLojaId(int id)
        {
            return _mapper.Map<LojaDto>(await _LojaRepository.ObterPorId(id));
        }
    }
}