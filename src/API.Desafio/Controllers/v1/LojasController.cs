using System.Threading.Tasks;
using API.Desafio.DTO;
using AutoMapper;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Desafio.Controllers.v1
{
    [AllowAnonymous]
    [Route("api/v1/lojas")]
    public class LojasController : MainController
    {
        private readonly ILojaRepository _LojaRepository;
        private readonly ILojaService _lojaService;
        private readonly IMapper _mapper;

        public LojasController(ILojaRepository lojaRepository,
                               IMapper mapper,
                               INotificador notificador,
                               ILojaService lojaService) : base(notificador)
        {
            _LojaRepository = lojaRepository;
            _mapper = mapper;
            _lojaService = lojaService;
        }

        private async Task<LojaDto> ObterLojaId(int id)
        {
            return _mapper.Map<LojaDto>(await _LojaRepository.ObterPorId(id));
        }

        [HttpGet("detalheLoja/{id:int}")]
        public async Task<ActionResult<LojaDto>> ObterPorId(int id)
        {
            var obj = await ObterLojaId(id);

            if (obj == null) return NotFound();

            return obj;
        }

        [HttpPost("adicionarLoja")]
        public async Task<ActionResult<LojaDto>> Adicionar(LojaDto obj)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _lojaService.Adicionar(_mapper.Map<Loja>(obj));

            return CustomResponse(obj);
        }

        [HttpPut("alterarLoja/{id:int}")]
        public async Task<ActionResult<LojaDto>> Atualizar(int id, LojaDto obj)
        {
            if (id != obj.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na requisção");
                return CustomResponse(obj);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _lojaService.Atualizar(_mapper.Map<Loja>(obj));

            return CustomResponse(obj);
        }

        [HttpDelete("excluirLoja/{id:int}")]
        public async Task<ActionResult<LojaDto>> Excluir(int id)
        {
            var obj = await ObterPorId(id);

            if (obj == null) return NotFound();

            await _lojaService.Remover(id);

            return CustomResponse(obj);
        }

    }
}