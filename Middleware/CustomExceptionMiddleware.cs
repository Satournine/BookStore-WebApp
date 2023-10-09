﻿using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {

            var stopwatch = Stopwatch.StartNew();
            try
            {

            await _next(context);
            stopwatch.Stop();
            string message = "[Respone] HTTP " + context.Request.Method + "-" + context.Request.Path + "Respone Code" + context.Response.StatusCode + "- " + "in " + stopwatch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);
            }
            catch(Exception ex)
            {
                stopwatch.Stop();
                await HandleException(context, ex, stopwatch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch stopwatch)
        {
            string message = "[Error] HTTP"+ context.Request.Method + "-" + context.Request.Path + "Error Message" + ex.Message + "- " + "in " + stopwatch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);

        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

}