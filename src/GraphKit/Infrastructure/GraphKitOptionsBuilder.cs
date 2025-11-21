using GraphKit.Core;
using GraphKit.Infrastructure.Internal;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GraphKit.Infrastructure
{
    public class GraphKitOptionsBuilder : IGraphKitOptionsBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public GraphKitOptionsBuilder(IServiceCollection services)
        {
            ServiceCollection = services;
        }

        public IGraphKitOptionsBuilder UseGraphKit(GraphKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AddProviderService(options);
            ServiceCollection.TryAdd(new ServiceDescriptor(typeof(IEmailService), typeof(EmailService), lifetime));
            ServiceCollection.TryAdd(new ServiceDescriptor(typeof(IEmailSender), typeof(EmailService), lifetime));
            return this;
        }

        private void AddProviderService(GraphKitOptions options)
        {
            ServiceCollection.TryAddSingleton<IGraphKitProvider>(x => new GraphKitProvider(options, x));
        }
    }
}
