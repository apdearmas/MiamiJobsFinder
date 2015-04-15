
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace BDL.Tests
{

    public class AzureStorageServiceTest
    {
        private AzureStorageService storageService;

        private Mock<ICloudStorageAccountWrapper> cloudStorageAccountWrapperMock;
        public AzureStorageServiceTest()
        {
            cloudStorageAccountWrapperMock = new Mock<ICloudStorageAccountWrapper>();
        }

        [Fact]
        public void CreateStorageService()
        {
            //arrange
            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(), false);
            cloudStorageAccountWrapperMock.Setup(m => m.Parse(It.IsAny<string>()))
                .Returns(cloudStorageAccount);

            //act
            storageService = new AzureStorageService(cloudStorageAccountWrapperMock.Object);

            //assert
            Assert.IsNotNull(storageService);
        }


        //[Fact]
        //public void VerifyUploadBlobIsCalledWithValidParameters()
        //{
        //    //arrange
        //    string fileName="xFileName";
        //    string path = "c:\a\b";

        //    //act
        //    storageServiceMock.Setup(m => m.UploadBlob(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
        //    storageService = new AzureStorageService(storageServiceMock.Object);
        //    storageService.InitCloudStorage();
        //    storageService.UploadBlob(fileName, path);

        //    //assert
        //    storageServiceMock.Verify(m=>m.UploadBlob(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        //}

        //[Fact]
        //public void VerifyDeleteBlobIsCalledWithValidParameters()
        //{
        //    //arrange
        //    storageServiceMock = new Mock<IAzureStorageService>();
        //    string fileName = "xFileName";

        //    //act
        //    storageServiceMock.Setup(m => m.DeleteBlob(It.IsAny<string>())).Verifiable();
        //    storageService = new AzureStorageService(storageServiceMock.Object);
        //    storageService.InitCloudStorage();
        //    storageService.DeleteBlob(fileName);

        //    //assert
        //    storageServiceMock.Verify(m => m.DeleteBlob(It.IsAny<string>()), Times.Once());
        //}

    }
}
