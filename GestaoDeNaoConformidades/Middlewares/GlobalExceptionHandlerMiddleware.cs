using FluentValidation;
using GestaoDeNaoConformidades.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly log4net.ILog _log4Net;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _log4Net = log4net.LogManager.GetLogger(typeof(GlobalExceptionHandlerMiddleware));
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);

                _log4Net.Error($"Unexpected error: {ex}");
                _logger.LogError($"Unexpected error: {ex}");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object json;

            if (exception is ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                json = new
                {
                    context.Response.StatusCode,
                    exception.Message,
                };

                return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
            }

            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                json = new
                {
                    context.Response.StatusCode,
                    exception.Message,
                };

                return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            json = new
            {
                context.Response.StatusCode,
                exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}
