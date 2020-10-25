using ApexScheduler.ApexStatus;

using Microsoft.Extensions.Logging;

using Raven.Client.Documents;
using Raven.Client.Documents.Session;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexScheduler.RavenDB
{
    public class RavenDBClient : IDbClient
    {
        private static Lazy<IDocumentStore> store;
        private readonly ILogger<RavenDBClient> _logger;

        private static IDocumentStore CreateStore(string[] urls, string databaseName)
        {
            IDocumentStore store = new DocumentStore
            {
                Urls = urls,
                Database = databaseName
            }.Initialize();

            return store;
        }

        public RavenDBClient(ILogger<RavenDBClient> logger, ApexConfigs config)
        {
            _logger = logger;
            store = new Lazy<IDocumentStore>(CreateStore(config.RavenDBConfig.Url, config.RavenDBConfig.DatabaseName));
        }

        public static IDocumentStore Store => store.Value;

        public async Task<StatusRoot> GetStatus(string apexId)
        {
            using (IAsyncDocumentSession session = Store.OpenAsyncSession("ApexClient"))
            {
                var result = await session.Query<StatusRoot>()
                                           .Where(p => p.ApexId == apexId)
                                           .ToListAsync();
                return result.First();
            }
        }

        public void Dispose()
        {
            Store.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}