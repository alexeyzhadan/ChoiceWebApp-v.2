using ChoiceWebApp.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ChoiceWebApp.Extensions
{
    public static class TopSecretExtension
    {
        public static IApplicationBuilder UseTopSecret(this IApplicationBuilder app)
            => app.UseMiddleware<TopSecret>();
    }
}