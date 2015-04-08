using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace BDL.Tests
{

    class AzureStorageServiceTest
    {
        private readonly AzureStorageService storageService;

        public AzureStorageServiceTest()
        {
            storageService = new AzureStorageService();
        }

        [Fact]
        public void CreateStorageService() 
        {   
            Assert.IsNotNull(storageService);
        }     

        [Fact]
        public void VerifyAzureStorageAutentication()
        {
            //arrange
            string accountName = null;
            string accessKey = null;
            

            //act
            //accountName = this.storageService.accountName;
            //accessKey = this.storageService.accessKey;
            //assert
            Assert.IsNotNull(accountName);
        }

    }
}
