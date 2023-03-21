using System.Net;
using System.Text.Json;
using FluentValidation;
using JetBrains.Annotations;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventsApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ScException e)
            {
                await HandleScExceptionAsync(context, e);
            }
            catch (ValidationException e)
            {
                await HandleValidationExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleScExceptionAsync(HttpContext context, ScException exception)
        {
            const HttpStatusCode code = HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(new ScResult(new ScError() { Message = exception.Message }));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            const HttpStatusCode code = HttpStatusCode.BadRequest;
            var result = new ScResult(new ScError()
            {
                Message = "Имееются некорректные данные",
                ModelState = new Dictionary<string, List<string>>()

            });
            foreach (var failure in exception.Errors)
            {
                if (!result.Error!.ModelState!.ContainsKey(failure.PropertyName))
                {
                    result.Error.ModelState.Add(failure.PropertyName, new List<string>());
                }

                result.Error.ModelState[failure.PropertyName].Add(failure.ErrorMessage);
            }

            var newResult = JsonSerializer.Serialize(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(newResult);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new ScResult(new ScError() { Message = $"Ошибка {exception.Source}: {exception.Message}" }));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
