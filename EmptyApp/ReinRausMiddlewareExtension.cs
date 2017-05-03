using System;
using Microsoft.Extensions.DependencyInjection;

namespace EmptyApp
{
    public static class ReinRausMiddlewareExtension
    {
        public static IServiceCollection AddReinRaus(this IServiceCollection serviceCollection,
            Action<ReinRausOptions> setupAction)
        {
            if (setupAction != null)
            {
                serviceCollection.Configure(setupAction);
            }
            return serviceCollection;
        }
    }

    public class ReinRausOptions
    {
        public string Nummer { get; set; } = string.Empty;
        public TimeSpan WaitTime { get; set; } = TimeSpan.Zero;
    }
}