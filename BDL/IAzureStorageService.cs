using System.IO;
using System.Web;

namespace BDL
{
    public interface IAzureStorageService
    {

        string UploadBlob(string filename, string contentType, Stream content);

        void DeleteBlob(string fileName);
    }
}
