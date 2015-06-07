using System.IO;
using System.Web;
using System;

namespace BDL
{
    public interface IAzureStorageService
    {

        string UploadBlob(string filename, string contentType, Stream content);

        void DeleteBlob(string fileName);

        Uri getAzureContainerUri();
    }
}
