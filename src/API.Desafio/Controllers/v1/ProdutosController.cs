using System.Threading.Tasks;
using API.Desafio.DTO;
using AutoMapper;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Desafio.Controllers.v1
{
    [Route("api/v1/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _ProdutoRepository;
        private readonly IProdutoService _ProdutoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository ProdutoRepository,
                               IMapper mapper,
                               INotificador notificador,
                               IProdutoService ProdutoService) : base(notificador)
        {
            _ProdutoRepository = ProdutoRepository;
            _mapper = mapper;
            _ProdutoService = ProdutoService;
        }

        private async Task<ProdutoDto> ObterProdutoId(int id)
        {
            return _mapper.Map<ProdutoDto>(await _ProdutoRepository.ObterPorId(id));
        }

        [HttpGet("detalheProduto/{id:int}")]
        public async Task<ActionResult<ProdutoDto>> ObterPorId(int id)
        {
            var obj = await ObterProdutoId(id);

            if (obj == null) return NotFound();

            return obj;
        }

        [HttpPost("adicionarProduto")]
        public async Task<ActionResult<ProdutoDto>> Adicionar(ProdutoDto obj)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ProdutoService.Adicionar(_mapper.Map<Produto>(obj));

            return CustomResponse(obj);
        }

        [HttpPut("alterarProduto/{id:int}")]
        public async Task<ActionResult<ProdutoDto>> Atualizar(int id, ProdutoDto obj)
        {
            if (id != obj.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na requisção");
                return CustomResponse(obj);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ProdutoService.Atualizar(_mapper.Map<Produto>(obj));

            return CustomResponse(obj);
        }

        [HttpDelete("excluirProduto/{id:int}")]
        public async Task<ActionResult<ProdutoDto>> Excluir(int id)
        {
            var obj = await ObterPorId(id);

            if (obj == null) return NotFound();

            await _ProdutoService.Remover(id);

            return CustomResponse(obj);
        }
    }
}