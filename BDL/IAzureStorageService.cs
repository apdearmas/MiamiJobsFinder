using System.Web;

namespace BDL
{
    public interface IAzureStorageService
    {

        string UploadBlob(HttpPostedFileBase jobOfferFileName);

        void DeleteBlob(string fileName);
    }
}
