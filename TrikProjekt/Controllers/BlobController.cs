using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
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
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=triknewblob;AccountKey=8dUYz4i9NIz0DzQY5XbBDEUFFAKGy5rOJIFMJyhP8xKZ3j0w63g/C/nDJSh/l73hCopTRzU/m/cf+AStY5v63A==;EndpointSuffix=core.windows.net");
                blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
                await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
                List<Uri> allBlobs = new List<Uri>();
                List<string> captions = new List<string>();
                foreach (BlobItem blob in blobContainer.GetBlobs())
                {
                    if (blob.Properties.BlobType == BlobType.Block)
                    {
                        BlobClient blobClient = blobContainer.GetBlobClient(blob.Name);
                        BlobProperties properties = await blobClient.GetPropertiesAsync();
                        foreach (var metadataItem in properties.Metadata)
                        {
                            captions.Add(metadataItem.Value.ToString());
                        }
                        allBlobs.Add(blobClient.Uri);
                    }
                }
                ViewBag.Captions = captions;
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
            ComputerVisionClient vision = new ComputerVisionClient(new ApiKeyServiceClientCredentials("1acfdcf3a3a2478584833e9082a25bf8"), new System.Net.Http.DelegatingHandler[] { });
            vision.Endpoint = "https://cvtrik.cognitiveservices.azure.com/";
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
                        List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>() { VisualFeatureTypes.Description };
                        var result = await vision.AnalyzeImageAsync(blob.Uri.ToString(), features);
                        IDictionary<string, string> metadata = new Dictionary<string, string>();
                        metadata.Add("Caption", result.Description.Captions[0].Text);
                        await blob.SetMetadataAsync(metadata);
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