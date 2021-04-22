using API.Desafio.DTO;
using AutoMapper;
using Business.Desafio.Models;

namespace API.Desafio.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Loja, LojaDto>().ReverseMap();
            CreateMap<Estoque, EstoqueDto>().ReverseMap();
            CreateMap<Produto, ProdutoDto>().ReverseMap();
        }
    }
}