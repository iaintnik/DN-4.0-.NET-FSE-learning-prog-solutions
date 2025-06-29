using NUnit.Framework;
using Moq;
using CustomerCommLib;

namespace CustomerCommTests
{
    [TestFixture]
    public class CustomerCommTests
    {
        [Test]
        public void SendMailToCustomer_ShouldReturnTrue_WhenMailSent()
        {
            var mockMailSender = new Mock<IMailSender>();
            mockMailSender.Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var customerComm = new CustomerComm(mockMailSender.Object);
            bool result = customerComm.SendMailToCustomer();
            Assert.IsTrue(result);
            mockMailSender.Verify(m => m.SendMail("cust123@abc.com", "Some Message"), Times.Once);
        }
    }
}
