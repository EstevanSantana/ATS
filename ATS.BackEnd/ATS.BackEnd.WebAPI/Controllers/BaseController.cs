using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.Service.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ATS.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IErrorMessageService _errorMessage;
        public BaseController(IErrorMessageService errorMessage)
        {
            _errorMessage = errorMessage;
        }

        protected ActionResult BaseView(object result = null)
        {
            if (Valid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _errorMessage.GetErrorMessages().Select(n => n.Mensagem)
            });
        }

        protected ActionResult BaseView(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyError(modelState);
            return BaseView();
        }

        protected void NotifyError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in errors)
            {
                var msg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                _errorMessage.Handler(new ErrorMessage(msg));
            }
        }

        protected bool Valid()
        {
            return !_errorMessage.HasError();
        }

        protected void NotifyError(string mensagem)
        {
            _errorMessage.Handler(new ErrorMessage(mensagem));
        }
    }
}
