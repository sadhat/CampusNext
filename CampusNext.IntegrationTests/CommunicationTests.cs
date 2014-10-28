using System;
using CampusNext.Communication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests
{
    [TestClass]
    public class CommunicationTests
    {
        [TestMethod]
        public void SendEmail()
        {
            IEmailSender emailSender = new EmailSender();
            emailSender.Send("sadhat@hotmail.com", new[]{"syahmed@microsoft.com"},"Testing","This is test from Syahmed");
        }
    }
}
