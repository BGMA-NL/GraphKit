using Azure.Identity;
using GraphKit.Async;
using GraphKit.Infrastructure.Internal;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;

namespace GraphKit
{
    public class GraphKitProvider : IGraphKitProvider
    {
        private GraphKitOptions Options { get; set; }
        private IServiceProvider ServiceProvider { get; }
        
        public GraphKitProvider(GraphKitOptions graphOptions, IServiceProvider serviceProvider)
        {
            Options = graphOptions;
            ServiceProvider = serviceProvider;
        }

        public GraphServiceClient GraphClient
        {
            get
            {
                return LazyAuthProvider().Value;
            }
        }

        public string SenderEmail => Options.SenderEmail ?? "";

        private Lazy<GraphServiceClient> LazyAuthProvider()
        {
            return new Lazy<GraphServiceClient>(() =>
            {
                return AsyncHelpers.RunSync(() => InitGraphServiceClient());
            });
        }

        private Task<GraphServiceClient> InitGraphServiceClient()
        {
            var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider(Options, ServiceProvider));
            var graphClient = new GraphServiceClient(authenticationProvider);

            return Task.FromResult(graphClient);
        }
    }
}
