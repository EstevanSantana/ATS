using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BackEnd.Service.Validation
{
    public class ErrorMessageService : IErrorMessageService
    {
        private List<ErrorMessage> _notify;
        public ErrorMessageService()
        {
            _notify = new List<ErrorMessage>();
        }

        public List<ErrorMessage> GetErrorMessages()
        {
            return _notify;
        }

        public void Handler(ErrorMessage notificacao)
        {
            _notify.Add(notificacao);
        }

        public bool HasError()
        {
            return _notify.Any();
        }
    }
}
