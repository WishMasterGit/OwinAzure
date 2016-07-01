using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace OWINAzure.Services
{
    public class PuzzleService
    {
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _table;

        public PuzzleService()
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            _tableClient = storageAccount.CreateCloudTableClient();

            _table = _tableClient.GetTableReference("puzzleData");
            _table.CreateIfNotExists();
        }

        public PuzzleEntity Create(PuzzleEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);
            _table.Execute(operation);
            return entity;
        }
        public PuzzleEntity Update(PuzzleEntity entity)
        {
            var operation = TableOperation.Replace(entity);
            _table.Execute(operation);
            return entity;
        }
        public PuzzleEntity Delete(PuzzleEntity entity)
        {
            var operation = TableOperation.Delete(entity);
            _table.Execute(operation);
            return entity;
        }
        public PuzzleEntity Get(string id)
        {
            var query = new TableQuery<PuzzleEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "global"))
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
            return _table.ExecuteQuery(query).FirstOrDefault();
        }

        public IEnumerable<PuzzleEntity> GetList()
        {
            var query = new TableQuery<PuzzleEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "global"));
            return _table.ExecuteQuery(query);

        }


    }
}