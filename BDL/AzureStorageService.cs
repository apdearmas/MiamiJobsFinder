using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.IO;

namespace BDL
{
    public interface IAzureStorageService
    {
        string UploadBlob(string fileName, string filePath);
        void DeleteBlob(string fileName);
    }

    public class AzureStorageService : IAzureStorageService
    {

        private CloudStorageAccount cloudStorageAccount;
        private CloudBlobContainer cloudBlobContainer;

        public AzureStorageService()
        {
            // Retrieve storage account from connection string.
            cloudStorageAccount = CloudStorageAccount.Parse(
                            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();


            // Retrieve a reference to a container. 
            cloudBlobContainer = blobClient.GetContainerReference("test");

            // Create the container if it doesn't already exist.
            if (cloudBlobContainer.CreateIfNotExists())
            {
                //Public access to container
                cloudBlobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }


            //UploadBlob("PAP201500222600201.pdf", "C:\\Users\\Lenovo\\Documents\\Frank\\INSPECTIONS\\4_2_15");
            //DeleteBlob("PAP201500222600201.pdf");
        }

        public string UploadBlob(string fileName, string filePath)
        {
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

            string targetFile =  Path.Combine(filePath,fileName);

            blockBlob.Properties.ContentType = "application/pdf";

            using (var fileStream = System.IO.File.OpenRead(targetFile))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            return blockBlob.Uri.ToString();
        }

        public void DeleteBlob(string fileName)
        {

            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            blockBlob.Delete();
        }
    }
}
