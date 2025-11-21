using GraphKit.Infrastructure;
using GraphKit.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GraphKit.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddGraphKit(this IServiceCollection serviceCollection, Action<GraphKitOptionsBuilder> optionsAction)
        {
            Check.Argument.IsNotNull(serviceCollection, nameof(serviceCollection), "IServiceCollection is not dependency injection");
            Check.Argument.IsNotNull(optionsAction, nameof(optionsAction));

            optionsAction.Invoke(new GraphKitOptionsBuilder(serviceCollection));

            return serviceCollection;
        }

    }
}
