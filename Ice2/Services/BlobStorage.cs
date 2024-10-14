using Ice2.Models;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

//contains the blob storage method for uploading the blob to azure blob storage
//https://www.youtube.com/watch?v=SIBqRVJyQF0 
//https://stackoverflow.com/questions/60617190/how-do-i-store-a-picture-in-azure-blob-storage-in-asp-net-mvc-application 

namespace Ice2.Services
{
    public class BlobStorage
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorage(string connectionString, string storageContainerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(storageContainerName);
            _blobContainerClient.CreateIfNotExists();

        }

        public async Task<string> UploadBlobAsync(string blobName, Stream content)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content);
            return blobClient.Uri.ToString();
        }
    }
}
