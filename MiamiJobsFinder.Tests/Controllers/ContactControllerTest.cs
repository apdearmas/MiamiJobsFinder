using System.Web.Mvc;
using MiamiJobsFinder.Controllers;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MiamiJobsFinder.Tests.Controllers
{
    public class ContactControllerTest
    {

        [Fact]
        public void Contact()
        {
            // Arrange
            var controller = new ContactController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}