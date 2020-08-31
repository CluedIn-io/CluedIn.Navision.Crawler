using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using System.Linq;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.Navision.Core;
using CluedIn.Crawling.Navision.Infrastructure.Factories;
using CluedIn.Providers.Models;
using Newtonsoft.Json;

namespace CluedIn.Provider.Navision
{
    public class NavisionProvider : ProviderBase, IExtendedProviderMetadata
    {
        private readonly INavisionClientFactory _navisionClientFactory;

        public NavisionProvider([NotNull] ApplicationContext appContext, INavisionClientFactory navisionClientFactory)
            : base(appContext, NavisionConstants.CreateProviderMetadata())
        {
            _navisionClientFactory = navisionClientFactory;
        }

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var navisionCrawlJobData = new NavisionCrawlJobData();
            if (configuration.ContainsKey(NavisionConstants.KeyName.ApiKey))
            { navisionCrawlJobData.ApiKey = configuration[NavisionConstants.KeyName.ApiKey].ToString(); }
            if (configuration.ContainsKey(NavisionConstants.KeyName.Url))
            { navisionCrawlJobData.Url = configuration[NavisionConstants.KeyName.Url].ToString(); }
            if (configuration.ContainsKey(NavisionConstants.KeyName.DeltaCrawlEnabled))
            { navisionCrawlJobData.DeltaCrawlEnabled = bool.Parse(configuration[NavisionConstants.KeyName.DeltaCrawlEnabled].ToString()); }
            if (configuration.ContainsKey(NavisionConstants.KeyName.UserName))
            { navisionCrawlJobData.UserName = configuration[NavisionConstants.KeyName.UserName].ToString(); }
            if (configuration.ContainsKey(NavisionConstants.KeyName.Password))
            { navisionCrawlJobData.Password = configuration[NavisionConstants.KeyName.Password].ToString(); }
            navisionCrawlJobData.ClientId = ConfigurationManager.AppSettings.GetValue<string>("Providers.NavisionClientId", null);
            navisionCrawlJobData.ClientSecret = ConfigurationManager.AppSettings.GetValue<string>("Providers.NavisionClientSecret", null);

            return await Task.FromResult(navisionCrawlJobData);
        }

        public override Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            [NotNull] CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var dictionary = new Dictionary<string, object>();

            if (jobData is NavisionCrawlJobData navisionCrawlJobData)
            {
                //TODO add the transformations from specific CrawlJobData object to dictionary
                // add tests to GetHelperConfigurationBehaviour.cs
                dictionary.Add(NavisionConstants.KeyName.ApiKey, navisionCrawlJobData.ApiKey);
                dictionary.Add(NavisionConstants.KeyName.Url, navisionCrawlJobData.Url);
                dictionary.Add(NavisionConstants.KeyName.DeltaCrawlEnabled, navisionCrawlJobData.DeltaCrawlEnabled);
                dictionary.Add(NavisionConstants.KeyName.UserName, navisionCrawlJobData.UserName);
                dictionary.Add(NavisionConstants.KeyName.Password, navisionCrawlJobData.Password);
            }

            return await Task.FromResult(dictionary);
        }

        public override Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {
            throw new NotImplementedException();
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            if (!(jobData is NavisionCrawlJobData navisionCrawlJobData))
            {
                throw new Exception("Wrong CrawlJobData type");
            }

            var client = _navisionClientFactory.CreateNew(navisionCrawlJobData);
            return await Task.FromResult(client.GetAccountInformation());
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            throw new NotImplementedException();
        }

        public override Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));

            throw new NotImplementedException();
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            if (!ids.Any())
            {
                throw new ArgumentException(nameof(ids));
            }

            throw new NotImplementedException();
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));


            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }

        // TODO Please see https://cluedin-io.github.io/CluedIn.Documentation/docs/1-Integration/build-integration.html
        public string Icon => NavisionConstants.IconResourceName;
        public string Domain { get; } = NavisionConstants.Uri;
        public string About { get; } = NavisionConstants.CrawlerDescription;
        public AuthMethods AuthMethods { get; } = NavisionConstants.AuthMethods;
        public IEnumerable<Control> Properties => null;
        public string ServiceType { get; } = JsonConvert.SerializeObject(NavisionConstants.ServiceType);
        public string Aliases { get; } = JsonConvert.SerializeObject(NavisionConstants.Aliases);
        public Guide Guide { get; set; } = new Guide
        {
            Instructions = NavisionConstants.Instructions,
            Value = new List<string> { NavisionConstants.CrawlerDescription },
            Details = NavisionConstants.Details

        };

        public string Details { get; set; } = NavisionConstants.Details;
        public string Category { get; set; } = NavisionConstants.Category;
        public new IntegrationType Type { get; set; } = NavisionConstants.Type;
    }
}
