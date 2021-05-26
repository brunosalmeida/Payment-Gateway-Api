using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Dto;
using PaymentGateway.Dto.Response;

namespace PaymentGateway.Api.Controllers
{
    public class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        // protected ActionResult CustomResponse(ModelStateDictionary modelState)
        // {
        //     var errors = modelState.Values.SelectMany(e => e.Errors);
        //     foreach (var error in errors)
        //     {
        //         AddProcessingError(error.ErrorMessage);
        //     }
        //
        //     return CustomResponse();
        // }

        // protected ActionResult CustomResponse(ValidationResult validationResult)
        // {
        //     foreach (var error in validationResult.Errors)
        //     {
        //         AddProcessingError(error.ErrorMessage);
        //     }
        //
        //     return CustomResponse();
        // }
        //
        // protected ActionResult CustomResponse(ResponseResult resposta)
        // {
        //     HasErrors(resposta);
        //
        //     return CustomResponse();
        // }

        // protected bool HasErrors(ResponseResult response)
        // {
        //     if (response == null || !response.Errors.Messages.Any()) return false;
        //
        //     foreach (var message in response.Errors.Messages)
        //     {
        //         AddProcessingError(message);
        //     }
        //
        //     return true;
        // }

        protected bool IsOperationValid()
        {
            return !Errors.Any();
        }

        protected void AddProcessingError(string error)
        {
            Errors.Add(error);
        }

        protected void CleanError()
        {
            Errors.Clear();
        }
    }
}