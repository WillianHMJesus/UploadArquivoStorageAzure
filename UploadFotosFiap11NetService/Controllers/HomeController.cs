using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadFotosFiap11NetService.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

namespace UploadFotosFiap11NetService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new List<ImageFile>();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            
            CloudBlobContainer container = blobClient.GetContainerReference("uploadfotos");
            
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                var imageFile = new ImageFile();

                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    imageFile.Name = blob.Name;
                    imageFile.Url = blob.Uri.ToString();

                    list.Add(imageFile);
                }
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase pic)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("uploadfotos");
            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(pic.FileName);

            blockBlob.UploadFromStream(pic.InputStream);
            
            return RedirectToAction("Index");
        }
    }
}