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

        public async Task Atualizar(Loja loja)
        {
            if (!ExecutarValidacao(new LojaValidation(), loja)) return;

            if (_lojaRepository.Buscar(f => f.Cnpj.Equals(loja.Cnpj) && f.Id != loja.Id).Result.Any())
            {
                Notificar("Já existe uma loja com este CNPJ infomado.");
                return;
            }

            await _lojaRepository.Atualizar(loja);
        }

        public async Task Remover(int id)
        {
            if (_lojaRepository.ObterPorId(id).Result.EstoqueList.Any())
            {
                Notificar("A loja possui produtos cadastrados!");
                return;
            }

            await _lojaRepository.Remover(id);
        }

        public void Dispose()
        {
            _lojaRepository?.Dispose();
        }

       
    }
}