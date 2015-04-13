using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
namespace BDL
{
    public interface IAzureStorageService
    {
        CloudStorageAccount cloudStorageAccount{ get; set; }
        CloudBlobClient cloudBlobClient{ get; set; }
        CloudBlobContainer cloudBlobContainer{ get; set; }

        

        string UploadBlob(string fileName, string filePath);
        void DeleteBlob(string fileName);
    }
}