
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.IO;

namespace BDL
{

    public interface ICloudStorageAccountWrapper
    {
        CloudStorageAccount Parse(string connectionString);
    }

    public class CloudStorageAccountWrapper : ICloudStorageAccountWrapper
    {

        public CloudStorageAccount Parse(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }
    }

    public class AzureStorageService : IAzureStorageService
    {
        private CloudStorageAccount cloudStorageAccount { get; set; }
        private CloudBlobClient cloudBlobClient { get; set; }
        private CloudBlobContainer cloudBlobContainer { get; set; }

        private ICloudStorageAccountWrapper cloudStorageAccountWrapper;

        public AzureStorageService(ICloudStorageAccountWrapper cloudStorageAccountWrapper)
        {
            this.cloudStorageAccountWrapper = cloudStorageAccountWrapper;

            // Retrieve storage account from connection string.
            cloudStorageAccount = cloudStorageAccountWrapper.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();


            // Retrieve a reference to a container. 
            cloudBlobContainer = cloudBlobClient.GetContainerReference("MJFContainer");

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

            string targetFile = Path.Combine(filePath, fileName);

            blockBlob.Properties.ContentType = "application/pdf";

            using (var fileStream = File.OpenRead(targetFile))
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
