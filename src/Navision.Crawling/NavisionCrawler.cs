using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Navision.Core;
using CluedIn.Crawling.Navision.Infrastructure.Factories;

namespace CluedIn.Crawling.Navision
{
    public class NavisionCrawler : ICrawlerDataGenerator
    {
        private readonly INavisionClientFactory clientFactory;
        public NavisionCrawler(INavisionClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is NavisionCrawlJobData navisioncrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(navisioncrawlJobData);

            //retrieve data from provider and yield objects
            
        }       
    }
}
