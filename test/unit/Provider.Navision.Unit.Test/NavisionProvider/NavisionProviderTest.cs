using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Navision.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.Navision.Unit.Test.NavisionProvider
{
    public abstract class NavisionProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<INavisionClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected NavisionProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<INavisionClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new Navision.NavisionProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
