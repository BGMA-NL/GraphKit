using Microsoft.Graph;

namespace GraphKit
{
    public interface IGraphKitProvider
    {
        GraphServiceClient GraphClient { get; }
        string SenderEmail { get; }
    }
}
