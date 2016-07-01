using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace OWINAzure.Services
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PuzzleEntity: TableEntity
    {
        [JsonProperty("id")]
        public string Id
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        [JsonProperty("data")]
        public string Data { get; set; }

        public PuzzleEntity()
        {
            PartitionKey = "global";
        }
    }
}