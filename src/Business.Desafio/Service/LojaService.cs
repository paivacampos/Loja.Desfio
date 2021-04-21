﻿using System;
using System.Linq;
using Business.Desafio.Interfaces;
using System.Threading.Tasks;
using Business.Desafio.Models;
using Business.Desafio.Models.Validations;

namespace Business.Desafio.Service
{
    public class LojaService : BaseService, ILojaService
    {
        private readonly ILojaRepository _lojaRepository;
        

        public LojaService(INotificador notificador, 
                           ILojaRepository lojaRepository) : base(notificador)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task Adicionar(Loja loja)
        {
            try
            {
                if (ExecutarValidacao(new LojaValidation(), loja))
                {
                    if (_lojaRepository.Buscar(f => f.Cnpj.Equals(loja.Cnpj)).Result.Any())
                    {
                        Notificar("Já existe uma loja com este CNPJ infomado.");
                        return;
                    }

                    await _lojaRepository.Adicionar(loja);
                }
            }
            catch (Exception e)
            {
                Notificar("Não foi possível realizar a operação no momento.");
            }
        }

        public async Task Atualizar(Loja loja)
        {
            try
            {
                if (!ExecutarValidacao(new LojaValidation(), loja)) return;

                if (_lojaRepository.Buscar(f => f.Cnpj.Equals(loja.Cnpj) && f.Id != loja.Id).Result.Any())
                {
                    Notificar("Já existe uma loja com este CNPJ infomado.");
                    return;
                }

                await _lojaRepository.Atualizar(loja);
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
                var estoqueExiste = _lojaRepository.ObterPorId(id).Result.EstoqueList.Any();
                if (estoqueExiste)
                {
                    Notificar("A loja possui produtos cadastrados!");
                    return;
                }

                await _lojaRepository.Remover(id);
            }
            catch (Exception e)
            {
                Notificar("A loja informada não está cadastrada!");
            }
        }

        public void Dispose()
        {
            _lojaRepository?.Dispose();
        }

       
    }
}