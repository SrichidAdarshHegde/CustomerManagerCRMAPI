using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FPL.Api.Controllers.Blob
{
    public class BlobDocuments
    {

        public static string GetDocumentorfileUri(HttpPostedFile fileorimage)
        {
            if (fileorimage.ContentLength > 0)
            {
                BlobStorage.UploadFile(fileorimage);
                string doiuri = BlobStorage.DocumentsorImagesUri.ToString();
                return doiuri;
            }
            return "Nill";
        }

        public static string GetDocumentorfileUri1(byte[] fileorimage, string filename)
        {
            if (fileorimage.Length > 0)
            {
                BlobStorage.UploadFile1(fileorimage, filename);
                string doiuri = BlobStorage.DocumentsorImagesUri.ToString();
                return doiuri;
            }
            return "Nill";
        }



        public static class BlobStorage
        {
            //public static string UploadFile(HttpPostedFileBase photoToUpload)
            //{

            //}



            internal static void UploadFile1(byte[] photoToUpload, string filename)
            {
                try
                {

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=accountdatastorage;AccountKey=/0cntcHDwClHBA5N6fkdMr/31ejNnjhkrf0Wzjyw2ZedAcUnDg8YHFqq1we4yaCHRUSp3+0Mbw5EtFrMNsU3Fw==;EndpointSuffix=core.windows.net");
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve a reference to a container.
                    CloudBlobContainer container = blobClient.GetContainerReference("pictures");

                    // Create the container if it doesn't already exist.
                    container.CreateIfNotExists();

                    container.SetPermissions(
                     new BlobContainerPermissions
                     {
                         PublicAccess = BlobContainerPublicAccessType.Blob
                     });
                    // Retrieve reference to a blob named "filename...".
                    string imageName = String.Format("task-photo-{0}{1}",
                     Guid.NewGuid().ToString(),
                     Path.GetExtension(filename));
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                    DocumentsorImagesUri = blockBlob.Uri;

                    //blockBlob.UploadFromStream(photoToUpload.InputStream);

                    byte[] data = photoToUpload;
                    using (var stream = new MemoryStream(data, writable: false))
                    {
                        blockBlob.UploadFromStream(stream);
                    }

                    //container.
                    // Create or overwrite the "filename..." blob with contents from a local file.
                    //using (var fileStream = System.IO.File.OpenRead("E:\\LatestWebApplication\\DISHA_14082015\\DISHADISHALATEST\\Portalfordifferentlyabledpeople\\WebImages\\" + fileName + ""))
                    //{                  

                    //}
                }
                catch (Exception ex)
                {

                }
            }


            public static Uri DocumentsorImagesUri { get; set; }

            internal static void UploadFile(HttpPostedFile photoToUpload)
            {
                try
                {

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=accountdatastorage;AccountKey=/0cntcHDwClHBA5N6fkdMr/31ejNnjhkrf0Wzjyw2ZedAcUnDg8YHFqq1we4yaCHRUSp3+0Mbw5EtFrMNsU3Fw==;EndpointSuffix=core.windows.net");
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve a reference to a container. 
                    CloudBlobContainer container = blobClient.GetContainerReference("pictures");

                    // Create the container if it doesn't already exist.
                    container.CreateIfNotExists();

                    container.SetPermissions(
                     new BlobContainerPermissions
                     {
                         PublicAccess = BlobContainerPublicAccessType.Blob
                     });
                    // Retrieve reference to a blob named "filename...".
                    string imageName = String.Format("task-photo-{0}{1}",
                     Guid.NewGuid().ToString(),
                     Path.GetExtension(photoToUpload.FileName));
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
                    DocumentsorImagesUri = blockBlob.Uri;
                    blockBlob.UploadFromStream(photoToUpload.InputStream);
                    //container.
                    // Create or overwrite the "filename..." blob with contents from a local file.
                    //using (var fileStream = System.IO.File.OpenRead("E:\\LatestWebApplication\\DISHA_14082015\\DISHADISHALATEST\\Portalfordifferentlyabledpeople\\WebImages\\" + fileName + ""))
                    //{                  

                    //}
                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}