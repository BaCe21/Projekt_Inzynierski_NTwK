using System.IO;
namespace TrikProjekt56.Controllers
{
    public class BlobController : Controller
    {
        const string blobContainerName = "webappstoragedotnet-imagecontainer";
        static BlobContainerClient blobContainer;
        /// <summary> 
        /// Task<ActionResult> Index() 
        /// Documentation References:  
        /// - What is a Storage Account: http://azure.microsoft.com/en-us/documentation/articles/storage-whatis-account/ 
        /// - Create a Storage Account: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#create-an-azure-storage-account
        /// - Create a Storage Container: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#create-a-container
        /// - List all Blobs in a Storage Container: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#list-the-blobs-in-a-container
        /// </summary> 
        public async Task<ActionResult> Index()
        {
            try
            {
                // Retrieve storage account information from connection string
                // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=trikblob;AccountKey=q63FoLT2VP6P6x+RVN271Mmcr1NWh1TXKcPdwrIlukQS8AW63tRygSMT9m1/dy4BzwzruksS+wto+AStwXN6FQ==;EndpointSuffix=core.windows.net");

                blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
                await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);

                // To view the uploaded blob in a browser, you have two options. The first option is to use a Shared Access Signature (SAS) token to delegate  
                // access to the resource. See the documentation links at the top for more information on SAS. The second approach is to set permissions  
                // to allow public access to blobs in this container. Comment the line below to not use this approach and to use SAS. Then you can view the image  
                // using: https://[InsertYourStorageAccountNameHere].blob.core.windows.net/webappstoragedotnet-imagecontainer/FileName 

                // Gets all Block Blobs in the blobContainerName and passes them to the view
                List<Uri> allBlobs = new List<Uri>();
                foreach (BlobItem blob in blobContainer.GetBlobs())
                {
                    if (blob.Properties.BlobType == BlobType.Block)
                        allBlobs.Add(blobContainer.GetBlobClient(blob.Name).Uri);
                }

                return View(allBlobs);
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadAsync()
        {
            try
            {
                var request = await HttpContext.Request.ReadFormAsync();
                if (request.Files == null)
                    { return BadRequest("Could not upload, files are null!"); }
                var files = request.Files;
                if (files.Count == 0)
                    { return BadRequest("Empty files!"); }
                for (int i = 0; i < files.Count; i++)
                    {
                        var blob = blobContainer.GetBlobClient(GetRandomBlobName(files[i].FileName));
                        var stream = files[i].OpenReadStream();
                        blob.Upload(stream);
                    }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        /// <summary> 
        /// Task<ActionResult> DeleteImage(string name) 
        /// Documentation References:  
        /// - Delete Blobs: https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/#delete-blobs
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult> DeleteImage(string name)
        {
            try
            {
                Uri uri = new Uri(name);
                string filename = Path.GetFileName(uri.LocalPath);

                var blob = blobContainer.GetBlobClient(filename);
                await blob.DeleteIfExistsAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}