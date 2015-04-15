namespace BDL
{
    public interface IAzureStorageService
    {

        string UploadBlob(string fileName, string filePath);
        void DeleteBlob(string fileName);
    }
}
