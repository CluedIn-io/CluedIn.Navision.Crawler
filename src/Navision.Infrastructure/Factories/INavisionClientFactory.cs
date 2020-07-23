using CluedIn.Crawling.Navision.Core;

namespace CluedIn.Crawling.Navision.Infrastructure.Factories
{
    public interface INavisionClientFactory
    {
        NavisionClient CreateNew(NavisionCrawlJobData navisionCrawlJobData);
    }
}
