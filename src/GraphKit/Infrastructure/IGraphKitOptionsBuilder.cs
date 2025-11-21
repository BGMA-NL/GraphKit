using GraphKit.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
namespace GraphKit.Infrastructure
{
    public interface IGraphKitOptionsBuilder
    {
        /// <summary>
        /// service collection
        /// </summary>
        IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// get redis options and add ConnectionMultiplexer to sercice collection
        /// </summary>
        /// <param name="options">redis options</param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        IGraphKitOptionsBuilder UseGraphKit(GraphKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped);
    }
}
