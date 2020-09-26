using SchoolAdministration.Services.Identity.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.WebUI.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            // If known exception, will use registered handler.
            TryHandleException(context);

            // If not, use default beahviour.
            base.OnException(context);
        }

        private void TryHandleServiceException(ExceptionContext context)
        {
            var message = "InternalServerError";
            context.Result = new ObjectResult(new ErrorCode<IList<string>>(new List<string>() { message })
            {

                Code = HttpStatusCode.InternalServerError,
                Message = message,
                Description = context.Exception.Message
            })
            { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }
        private void TryHandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
                _exceptionHandlers[type].Invoke(context);
            else
                TryHandleServiceException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            context.Result = new BadRequestObjectResult(new ErrorCode<IList<string>>(exception.Errors.SelectMany(s => s.Value).ToList())
            {
                Code = HttpStatusCode.BadRequest,
                Message = "One or more validation errors occurred.",
                Description = "One or more validation errors occurred."
            });

            context.ExceptionHandled = true;
        }
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
    public class ErrorCode<T>
    {
        public ErrorCode(T errors)
        {
            Errors = errors;
        }
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Description { get; set; }
        public T Errors { get; set; }
    }
}
