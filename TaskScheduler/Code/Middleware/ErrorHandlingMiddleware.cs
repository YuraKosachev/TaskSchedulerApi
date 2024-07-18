using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using TaskScheduler.Core.Exceptions;
using TaskScheduler.Core.Models.Response;
using TaskScheduler.Core.Constants;

namespace TaskScheduler.Code.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorList = new ErrorList();

            if (exception is ItemNotFoundException || exception is Exception)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorList.Errors = new List<Error> { new Error { Message = exception.Message } };
            }

            if (exception is FluentValidation.ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorList.Errors = (exception as FluentValidation.ValidationException)
                    .Errors
                    .Select(er => new Error { Message = er.ErrorMessage, Type = er.ErrorCode })
                    .ToList();
            }


            var result = JsonConvert.SerializeObject(errorList);
            context.Response.ContentType = ContentTypeConstants.ApplicationJson;
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
