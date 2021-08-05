using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.Service.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace ATS.BackEnd.Service.Services
{
    public abstract class BaseService
    {
        private readonly IErrorMessageService _errorMessage;

        public BaseService(IErrorMessageService errorMessage)
        {
            _errorMessage = errorMessage;
        }

        protected void Notify(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                _errorMessage.Handler(new ErrorMessage(error.ErrorMessage));
            }
        }

        protected bool RunValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
