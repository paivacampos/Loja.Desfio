using Business.Desafio.Interfaces;

namespace API.Desafio.Controllers
{
    public class AutenticacaoController : MainController
    {
        public AutenticacaoController(INotificador notificador) : base(notificador)
        {
        }
    }
}