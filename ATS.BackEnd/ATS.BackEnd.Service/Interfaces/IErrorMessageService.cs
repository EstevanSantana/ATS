using ATS.BackEnd.Service.Validation;
using System.Collections.Generic;

namespace ATS.BackEnd.Service.Interfaces
{
    public interface IErrorMessageService
    {
        void Handler(ErrorMessage notificacao);
        List<ErrorMessage> GetErrorMessages();
        bool HasError();
    }
}
