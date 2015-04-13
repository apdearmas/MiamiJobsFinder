using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace BDL.Tests
{

    class AzureStorageServiceTest
    {
        //private readonly AzureStorageService storageService;
        private Mock<IAzureStorageService> storageServiceMock;
        private AzureStorageService storageService;


        public AzureStorageServiceTest()
        {
           
        }

        [Fact]
        public void CreateStorageService() 
        {   
            //arrange
            storageServiceMock = new Mock<IAzureStorageService>();

            //act
            storageService = new AzureStorageService(storageServiceMock.Object);
            
            //assert
            Assert.IsNotNull(storageService);
        }     

        [Fact]
        public void VerifyAzureStorageAutentication()
        {
            //arrange
            storageServiceMock = new Mock<IAzureStorageService>();

            //act
            storageService = new AzureStorageService(storageServiceMock.Object);
            storageService.InitCloudStorage();

            //assert
            Assert.IsNotNull(storageService.cloudStorageAccount);
            Assert.IsNotNull(storageService.cloudBlobClient);
            Assert.IsNotNull(storageService.cloudBlobContainer);
        }

        [Fact]
        public void VerifyUploadBlobIsCallWithValidParameters()
        {
            //arrange
            storageServiceMock = new Mock<IAzureStorageService>();

            //act
            storageService = new AzureStorageService(storageServiceMock.Object);
            storageService.InitCloudStorage();
            //assert

        }

    }
}
