using GraphKit.Infrastructure;
using GraphKit.Infrastructure.Internal;
using GraphKit.Shared;
using Microsoft.Extensions.DependencyInjection;
namespace GraphKit.Extensions
{
    public static class GraphKitOptionsBuilderExtension
    {
        public static IGraphKitOptionsBuilder UseGraphKit(IGraphKitOptionsBuilder builder, GraphKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Check.Argument.IsNotNull(builder, nameof(builder), "The GraphKitOptionsBuilder is null");
            Check.Argument.IsNotNull(options, nameof(options), "The GraphKitOptions is null");

            return builder.UseGraphKit(options, lifetime);
        }

    }
}
