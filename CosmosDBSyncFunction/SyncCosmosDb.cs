using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CosmosDBSyncFunction
{
    public static class SyncCosmosDb
    {
        [FunctionName(nameof(Sync))]
        public static void Sync(
            [CosmosDBTrigger(
                databaseName: "evaluation",
                collectionName: "customer",
                ConnectionStringSetting = "cosmos-mm-eval",
                LeaseCollectionName = "leases",
                CreateLeaseCollectionIfNotExists=true
            )]IReadOnlyList<dynamic> input,
            [CosmosDB(
                databaseName: "evaluation",
                collectionName: "customer-by-country",
                ConnectionStringSetting = "cosmos-mm-eval")] IAsyncCollector< dynamic> output,
            ILogger log)

        {

            foreach (var item in input)
            {
                output.AddAsync(item);
            }
        }
    }
}
