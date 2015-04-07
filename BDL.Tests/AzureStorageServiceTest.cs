using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace BDL.Tests
{

    class AzureStorageServiceTest
    {
        private  AzureStorageService storageService;

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
            Assert.IsNotNull(accessKey);


        }

    }
}
