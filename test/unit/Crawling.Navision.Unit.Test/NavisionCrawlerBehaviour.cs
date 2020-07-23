using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Navision;
using CluedIn.Crawling.Navision.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Navision.Unit.Test
{
    public class NavisionCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public NavisionCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<INavisionClientFactory>();

            _sut = new NavisionCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
