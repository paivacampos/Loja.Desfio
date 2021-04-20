using FluentValidation;

namespace Business.Desafio.Models.Validations
{
    public class EstoqueValidation : AbstractValidator<Estoque>
    {
        public EstoqueValidation()
        {
            RuleFor(f => f.Quantidade)
                .LessThan(1).WithMessage("O campo {PropertyName} precisa ser fornecido")
                .WithMessage("O campo {PropertyName} precisa ter 1 item cadastrador");
        }
    }
}