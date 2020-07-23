using CluedIn.Core;
using CluedIn.Crawling.Navision.Core;

using ComponentHost;

namespace CluedIn.Crawling.Navision
{
    [Component(NavisionConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class NavisionCrawlerComponent : CrawlerComponentBase
    {
        public NavisionCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

