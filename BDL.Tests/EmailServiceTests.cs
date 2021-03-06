﻿using Moq;
using System.Collections.Generic;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BDL.Tests
{
    public class EmailServiceTests
    {
        private Mock<ISmtpClientWrapper> smtpClientWrapperMock;
        private EmailService emailService;

        public EmailServiceTests()
        {
            smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();
        }

        [Fact]       
        public void CreateEmailServiceObject()
        {
            emailService = new EmailService(smtpClientWrapperMock.Object);
            Assert.IsNotNull(emailService);
        }

        [Fact]
        public void VerifySendIsCalled()
        {
            smtpClientWrapperMock.Setup(m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            
            emailService = new EmailService(smtpClientWrapperMock.Object);
            
            emailService.Send(new List<string>(), string.Empty, string.Empty);

            smtpClientWrapperMock.Verify(m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        
    }
}
