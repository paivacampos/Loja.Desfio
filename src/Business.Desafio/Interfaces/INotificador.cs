using System.Collections.Generic;
using Business.Desafio.Notificacoes;

namespace Business.Desafio.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}