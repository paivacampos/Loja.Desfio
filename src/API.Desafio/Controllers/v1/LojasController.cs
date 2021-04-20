using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Desafio.Controllers
{
    [Route("api/v1/lojas")]
    public class LojasController: MainController
    {
        private readonly ILojaRepository _LojaRepository;
        private readonly IMapper _mapper;

        public LojasController(ILojaRepository lojaRepository, 
                               IMapper mapper)
        {
            _LojaRepository = lojaRepository;
            _mapper = mapper;
        }

        public async Task<JsonResult> ListarTodasLojas()
        {
            var objLst = _mapper<IEnumerable<Loja>>(await _LojaRepository.ObterTodos());
            return ;
        }
    }
}