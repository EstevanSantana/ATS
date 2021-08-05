using ATS.BackEnd.Domain.Models;
using FluentValidation;
using System;

namespace ATS.BackEnd.Service.Validation
{
    public class CandidatoValidation : AbstractValidator<Candidato>
    {
        public CandidatoValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 25).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");

            RuleFor(f => f.SobreNome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}.");

            RuleFor(f => f.Telefone)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(10, 15).WithMessage("O campo {PropertyName} precisa ter {MinLength} numeros.");

            RuleFor(f => CpfValidation.IsCpf(f.Cpf)).Equal(true)
                .WithMessage("Informe um CPF Valido");

            RuleFor(f => f.Cpf)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(10, 15).WithMessage("O campo {PropertyName} precisa ter {MinLength} numeros.");

            RuleFor(f => f.DataNascimento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");
        }
    }
}
