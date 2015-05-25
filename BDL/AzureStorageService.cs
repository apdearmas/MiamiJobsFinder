
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.IO;
using System;

namespace BDL
{
    public class AzureStorageService : IAzureStorageService
    {
        private static AzureStorageService _instance;
        private readonly CloudStorageAccount CloudStorageAccount;
        private readonly CloudBlobContainer CloudBlobContainer;

        private AzureStorageService()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient cloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();


            // Retrieve a reference to a container. 
            CloudBlobContainer = cloudBlobClient.GetContainerReference("mjfcontainer");

            // Create the container if it doesn't already exist.
            if (CloudBlobContainer.CreateIfNotExists())
            {
                //Public access to container
                CloudBlobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
        }

        public static AzureStorageService Instance
        {
            get { return _instance ?? (_instance = new AzureStorageService()); }
        }

        public string UploadBlob(string filename, string contentType, Stream content)
        {
            CloudBlockBlob blockBlob = CloudBlobContainer.GetBlockBlobReference(filename);

            blockBlob.Properties.ContentType = contentType;

            blockBlob.UploadFromStream(content);

            return blockBlob.Uri.ToString();
        }

        

        public void DeleteBlob(string fileName)
        {

            CloudBlockBlob blockBlob = CloudBlobContainer.GetBlockBlobReference(fileName);
            blockBlob.Delete();
        }

        public  Uri getAzureContainerUri()
        {
            return this.CloudBlobContainer.Uri;
        }
    }
}
