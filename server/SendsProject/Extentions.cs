using SendsProject.Middleware;

namespace SendsProject
{
    public static class Extentions
    {
        public static IApplicationBuilder UseShabbatMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShabbatMiddlware>();
        }
    }
}
