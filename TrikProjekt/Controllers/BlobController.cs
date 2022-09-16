using System.IO;
namespace TrikProjekt56.Controllers
{
    // Source
    // https://docs.microsoft.com/pl-pl/samples/azure-samples/storage-blobs-dotnet-webapp/net-photo-gallery-web-application-sample-with-azure-blob-storage/
    public class BlobController : Controller
    {
        const string blobContainerName = "webappstoragedotnet-imagecontainer";
        static BlobContainerClient blobContainer;
        public async Task<ActionResult> Index()
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=trikblob;AccountKey=q63FoLT2VP6P6x+RVN271Mmcr1NWh1TXKcPdwrIlukQS8AW63tRygSMT9m1/dy4BzwzruksS+wto+AStwXN6FQ==;EndpointSuffix=core.windows.net");
                blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
                await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
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

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}