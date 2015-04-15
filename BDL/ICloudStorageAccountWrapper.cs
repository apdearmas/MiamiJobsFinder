using Microsoft.WindowsAzure.Storage;

namespace BDL
{
    public interface ICloudStorageAccountWrapper
    {
        CloudStorageAccount Parse(string connectionString);
    }
}