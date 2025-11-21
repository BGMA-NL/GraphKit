using GraphKit.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions.Authentication;

namespace GraphKit;

public class TokenProvider : IAccessTokenProvider
{
    private readonly GraphKitOptions _options;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TokenProvider> _logger;

    public TokenProvider(GraphKitOptions options, IServiceProvider serviceProvider)
    {
        _options = options;
        _serviceProvider = serviceProvider;
        _logger = serviceProvider.GetRequiredService<ILogger<TokenProvider>>();
    }
    
    public TokenProvider(GraphKitOptions options, AllowedHostsValidator allowedHostsValidator)
    {
        _options = options;
    }

    public async Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };

        var confidentialClient = ConfidentialClientApplicationBuilder
            .Create(_options.ClientId)
            .WithClientSecret(_options.ClientSecret)
            .WithAuthority(new Uri($"https://login.microsoftonline.com/{_options.TenantId}/v2.0"))
            .Build();

        AuthenticationResult authResult;

        try
        {
            authResult = await confidentialClient
                .AcquireTokenForClient(scopes)
                .ExecuteAsync(cancellationToken).ConfigureAwait(false);
        }
        catch(Exception e)
        {
            _logger.LogError("Could not login to microsoft graph: {stackTrace}", e.StackTrace);
            throw;
        }

        return authResult.AccessToken;
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }
}