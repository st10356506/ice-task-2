using Azure;
using Azure.Data.Tables;

namespace Ice2.Models
{
    public class Data : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ImageUrl { get; set; } 
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

    }
}
