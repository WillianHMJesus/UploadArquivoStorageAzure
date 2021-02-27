using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UploadFotosFiap11NetService.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.Linq;

namespace UploadFotosFiap11NetService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var imageFiles = new List<ImageFile>();            
            var container = CloudStorage.GetContainer("uploadfotos");

            foreach (IListBlobItem blobItem in container.ListBlobs(null, false))
            {
                if (blobItem.GetType() == typeof(CloudBlockBlob))
                {
                    var blob = (CloudBlockBlob)blobItem;
                    var imageFile = new ImageFile(blob.Name, blob.Uri.ToString());

                    imageFiles.Add(imageFile);
                }
            }

            return View(imageFiles);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase pic)
        {
            CloudStorage.Add("uploadfotos", pic.FileName, pic.InputStream);
            
            return RedirectToAction("Index");
        }
    }
}