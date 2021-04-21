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
    public class EstoquesController :MainController
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IEstoqueService _Estoqueservice;
        private readonly IMapper _mapper;

        public EstoquesController( 
                               IMapper mapper,
                               INotificador notificador,
                               IEstoqueRepository estoqueRepository, 
                               IEstoqueService estoqueservice) : base(notificador)
        {
            _mapper = mapper;
            _estoqueRepository = estoqueRepository;
            _Estoqueservice = estoqueservice;
        }

        private async Task<EstoqueDto> ObterEstoqueId(int id)
        {
            return _mapper.Map<EstoqueDto>(await _estoqueRepository.ObterPorId(id));
        }

        [HttpGet("detalheEstoque/{id:int}")]
        public async Task<ActionResult<EstoqueDto>> ObterPorId(int id)
        {
            var obj = await ObterEstoqueId(id);

            if (obj == null) return NotFound();

            return obj;
        }

        [HttpPost("adicionarEstoque")]
        public async Task<ActionResult<EstoqueDto>> Adicionar(EstoqueDto obj)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _Estoqueservice.Adicionar(_mapper.Map<Estoque>(obj));
            obj.UltimaAtualizacao = DateTime.Now;
            return CustomResponse(obj);
        }

        [HttpPut("alterarEstoque/{id:int}")]
        public async Task<ActionResult<EstoqueDto>> Atualizar(int id, EstoqueDto obj)
        {
            if (id != obj.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na requisção");
                return CustomResponse(obj);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            obj.UltimaAtualizacao = DateTime.Now;
            
            await _Estoqueservice.Atualizar(_mapper.Map<Estoque>(obj));

            return CustomResponse(obj);
        }

        [HttpDelete("excluirEstoque/{id:int}")]
        public async Task<ActionResult<EstoqueDto>> Excluir(int id)
        {
            var obj = await ObterPorId(id);

            if (obj == null) return NotFound();

            await _Estoqueservice.Remover(id);

            return CustomResponse(obj);
        }
    }
}