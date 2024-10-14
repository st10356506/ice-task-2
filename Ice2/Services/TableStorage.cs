using Azure.Data.Tables;
using Ice2.Models;
using Azure;

namespace Ice2.Services
{
    public class TableStorage
    {
        private readonly TableClient _tableClient;

        //table storage service containing methods for inserting data into azure table storage container
        //https://www.c-sharpcorner.com/article/azure-storage-crud-operations-in-mvc-using-c-sharp-azure-table-storage-part-one/ 
        public TableStorage(string connectionString, string containerName)
        {
            var serviceClient = new TableServiceClient(connectionString);
            _tableClient = serviceClient.GetTableClient("TableData");

            _tableClient.CreateIfNotExists();

        }
        public async Task InsertDataAsync(Data data)
        {
            await _tableClient.AddEntityAsync(data);
        }
    }

}
