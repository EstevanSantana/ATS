using ATS.BackEnd.Domain.Models;
using FluentValidation;

namespace ATS.BackEnd.Service.Validation
{
    public class CurriculoValidation : AbstractValidator<Curriculo> 
    {
        public CurriculoValidation()
        {
            RuleFor(f => f.Conclusao)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");
            
            RuleFor(f => f.CurriculoImagem)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(f => f.Curso)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
              .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");
            
            RuleFor(f => f.Faculdade)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
              .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");
        }
    }
}
