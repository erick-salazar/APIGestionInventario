using APIGestionInventario.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIGestionInventario.Middleware
{
    public class CustomError : Exception
    {
        public string? Code { get; set; }
        public int StatusCode { get; set; }
        public List<ErrorDetail>? ErrorDetails {get;set;}

        public CustomError(int statusCode, string? code, string message, List<ErrorDetail>? errorDetails) : base(message)
        {
            StatusCode = statusCode;
            Code = code;
            ErrorDetails = errorDetails;            
        }
    }

    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _IServiceProvider;

        public CustomErrorHandlingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _IServiceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomError ex)
            {
                await HandleExceptionAsync(context, ex, ex.StatusCode, ex.Code, ex.Message, ex.ErrorDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, 401, "0003", "Solicitud no autorizada", null);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, 500, "001", "API server error", null);
            }
        }


        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            int statusCode,
            string? code,
            string errorMessage,
            List<ErrorDetail>? errorDetails
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            if (string.IsNullOrEmpty(code))
            {
                switch (statusCode)
                {
                    case 500:
                        code = "0001";
                        errorMessage = errorMessage.Length == 0 ? "API server error" : errorMessage;
                        break;
                    case 400:
                        code = "0002";
                        errorMessage = errorMessage.Length == 0 ? "Parametros de solicitud no validos" : errorMessage;
                        break;                    
                }
            }

            var response = new ResponseGenericAPI<object>
            {
                Status = context.Response.StatusCode,
                Code = code, 
                Message = errorMessage,
                Data = null,
                Error = errorDetails != null ? errorDetails :
            [
                new() { Field = "General", Message = exception.Message }
            ],
                TraceId = Activity.Current?.Id ?? context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(response);

        }
    }
}
