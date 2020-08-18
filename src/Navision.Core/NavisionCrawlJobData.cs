using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.Navision.Core
{
    public class NavisionCrawlJobData : CrawlJobData
    {
        public string ApiKey { get; set; }
        public string Table { get; set; }
        public string ConnectionString { get; set; }
    }
}
