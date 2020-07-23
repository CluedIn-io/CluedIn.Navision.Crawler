using CluedIn.Crawling.Navision.Core;

namespace CluedIn.Crawling.Navision
{
    public class NavisionCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<NavisionCrawlJobData>
    {
        public NavisionCrawlerJobProcessor(NavisionCrawlerComponent component) : base(component)
        {
        }
    }
}
