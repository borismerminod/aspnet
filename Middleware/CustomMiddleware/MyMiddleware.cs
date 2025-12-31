
namespace Middleware.CustomMiddleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            //Code appelé avant l'appel à la méthode next
            await context.Response.WriteAsync("Custom Middleware started\n");

            await next(context);
            //Code appelé après l'appel à la méthode next

            await context.Response.WriteAsync("\nCustom Middleware finished\n");

        }


    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder MyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyMiddleware>();
            return app;
        }
    }
}
