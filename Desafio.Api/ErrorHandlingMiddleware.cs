using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Desafio.Application.exception;

namespace Desafio.Api;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            var code = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred. Please try again later.";

            if (ex is DomainException)
            {
                code = HttpStatusCode.BadRequest;
                message = ex.Message;
            }

            var response = new
            {
                StatusCode = (int)code,
                Message = message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}