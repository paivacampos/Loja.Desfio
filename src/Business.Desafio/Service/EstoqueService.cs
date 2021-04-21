﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Desafio.Interfaces;
using Business.Desafio.Models;
using Business.Desafio.Models.Validations;

namespace Business.Desafio.Service
{
    public class EstoqueService : BaseService, IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(INotificador notificador,
                              IEstoqueRepository estoqueRepository) : base(notificador)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task Adicionar(Estoque estoque)
        {
            if (ExecutarValidacao(new EstoqueValidation(), estoque))
            {
                if (_estoqueRepository.Buscar(f => f.Id == estoque.Id).Result.Any())
                {
                    Notificar("Já existe um estoque informado para este produto.");
                    return;
                }

                await _estoqueRepository.Adicionar(estoque);
            }
        }

        public async Task Atualizar(Estoque estoque)
        {
            if (_estoqueRepository.Buscar(f => f.Id != estoque.Id).Result.Any())
            {
                Notificar("Já existe um estoque informado para este produto.");
                return;
            }

            await _estoqueRepository.Atualizar(estoque);
        }

        public async Task Remover(int id)
        {
            IEnumerable<Estoque> estoqueLst = _estoqueRepository.Buscar(f => f.Id == id).Result.ToList();

            if (!estoqueLst.Any())
            {
                Notificar("Não existem item estoque a ser excluído.");
                return;
            }

            await _estoqueRepository.Remover(id);
        }

        public void Dispose()
        {
            _estoqueRepository.Dispose();
        }
    }
}