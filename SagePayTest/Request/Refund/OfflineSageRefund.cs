using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    [TestFixture]
    public class OfflineSageRefund
    {
        [TestFixture]
        internal class Constructor
        {
            [Test]
            public void ConfigureFromConfig()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                var section = SagePay.Configuration.SageConfiguration.GetSection(ProviderTypes.Offline);

                Assert.AreEqual(section.VendorName, request.Vendor.VendorName);
            }

            [Test]
            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new SagePay.Request.Refund.OfflineSageRefund(name);

                Assert.AreEqual(name, request.Vendor.VendorName);
            }
        }

        [TestFixture]
        internal class Validate
        {
            [Test]
            public void ReturnsErrors()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = new SagePay.Request.Refund.RefundRequest();
                var errors = request.Validate();

                Assert.IsNotEmpty(errors);
            }

            [Test]
            public void SetsIsValid()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = RefundRequest.SampleRequest();
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
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                var response = request.Send();
            }

            [Test]
            public void EmitsAResponseIfValid()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = RefundRequest.SampleRequest();
                request.Validate();

                var response = request.Send();
                Assert.IsNotNull(response);
            }
        }
    }
}
