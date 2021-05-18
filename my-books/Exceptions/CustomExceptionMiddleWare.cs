using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using my_books.Data.Models.Views;

namespace my_books.Exceptions
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorViewMode()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from the middleware.",
                Path = "path"
            };
            
            return httpContext.Response.WriteAsync(response.ToString()); 
        }
    }
}