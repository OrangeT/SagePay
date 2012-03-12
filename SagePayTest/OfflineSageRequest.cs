using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class OfflineSageRequest
    {
        [TestFixture]
        internal class Constructor
        {
            [Test]
            public void ConfigureFromConfig()
            {
                var request = new SagePay.OfflineSageRequest();
                var section = SagePay.SageConfiguration.GetSection(SageConfiguration.CONFIG_SECTION);


                Assert.AreEqual(section.VendorName, request.Vendor.VendorName);
            }

            [Test]
            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new SagePay.OfflineSageRequest(name);

                Assert.AreEqual(name, request.Vendor.VendorName);
            }
        }

        [TestFixture]
        internal class Validate
        {
            [Test]
            public void ReturnsErrors()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                request.Transaction.CV2 = "12";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CV2", errors.First().Field);
                Assert.IsFalse(request.IsValid);
            }

            [Test]
            public void SetsIsValid()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                var errors = request.Validate();

                Assert.AreEqual(0, errors.Count);
                Assert.IsTrue(request.IsValid);
            }
        }

        [TestFixture]
        internal class Send
        {
            [Test]
            [AssertException(typeof (SageException))]
            public void ThrowsAnExceptionIfInvalid()
            {
                var request = new SagePay.OfflineSageRequest();
                var response = request.Send();
            }

            [Test]
            public void EmitsAResponseIsValid()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                request.Validate();

                var response = request.Send();
                Assert.IsNotNull(response);
            }
        }
    }
}

