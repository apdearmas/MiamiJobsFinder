
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.IO;

namespace BDL
{
    public class AzureStorageService : IAzureStorageService
    {
        private CloudStorageAccount CloudStorageAccount { get; set; }
        private CloudBlobContainer CloudBlobContainer { get; set; }

        public AzureStorageService()
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

        public string UploadBlob(HttpPostedFileBase jobOfferFileName)
        {
            CloudBlockBlob blockBlob = CloudBlobContainer.GetBlockBlobReference(jobOfferFileName.FileName);

            blockBlob.Properties.ContentType = jobOfferFileName.ContentType;

            blockBlob.UploadFromStream(jobOfferFileName.InputStream);

            return blockBlob.Uri.ToString();
        }

        

        public void DeleteBlob(string fileName)
        {

            CloudBlockBlob blockBlob = CloudBlobContainer.GetBlockBlobReference(fileName);
            blockBlob.Delete();
        }
    }
}
