using Microsoft.AspNetCore.Builder;

namespace my_books.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}