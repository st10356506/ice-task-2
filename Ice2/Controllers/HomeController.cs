using Ice2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure.Storage.Blobs;
using Azure.Data.Tables;
using Ice2.Services;

namespace Ice2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobStorage blobStorage;
        private readonly TableStorage tableStorage;

        public HomeController(ILogger<HomeController> logger, BlobStorage _blobStorage, TableStorage _tableStorage)
        {
            _logger = logger;
            blobStorage = _blobStorage;
            tableStorage = _tableStorage;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadData(IFormFile file, string name)
        {
            if (file != null && file.Length > 0)
            {
                //generate the blob name from the file name
                string blobName = file.FileName;

                //read the file content as a stream
                using var content = file.OpenReadStream();

                //upload to Blob Storage
                string imageUrl = await blobStorage.UploadBlobAsync(blobName, content);

                //create and insert data into table storage
                var data = new Data
                {
                    PartitionKey = "Profile",
                    Name = name,
                    ImageUrl = imageUrl
                };

                await tableStorage.InsertDataAsync(data);

                ViewBag.Message = $"Hello world, {name}";
                ViewBag.ImageUrl = imageUrl;
            }
            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
