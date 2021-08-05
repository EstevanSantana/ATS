using ATS.BackEnd.Domain.Models;
using FluentValidation;

namespace ATS.BackEnd.Service.Validation
{
    public class EnderecoValidation : AbstractValidator<Endereco> 
    {
        public EnderecoValidation()
        {
            RuleFor(f => f.Bairro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");

            RuleFor(f => f.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(9, 15).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");

            RuleFor(f => f.Cidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");
        
            RuleFor(f => f.Estado)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 2).WithMessage("O campo {PropertyName} precisa ter {MinLength}.");
        
            RuleFor(f => f.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(1, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");

            RuleFor(f => f.Rua)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");
        }
    }
}
