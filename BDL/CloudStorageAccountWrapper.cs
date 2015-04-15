using Microsoft.WindowsAzure.Storage;

namespace BDL
{
    public class CloudStorageAccountWrapper : ICloudStorageAccountWrapper
    {

        public CloudStorageAccount Parse(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}