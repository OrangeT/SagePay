using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    class RefundRequest
    {
        [TestFixture]
        internal class Validate
        {
            [Test]
            public void AllRequiredFields()
            {
                var request = SampleRequest();
                var errors = request.Validate();

                Assert.AreEqual(0, errors.Count);
            }

            [Test]
            public void MissingFields()
            {
                var request = new SagePay.Request.Refund.RefundRequest();
                var errors = request.Validate();

                var keys = errors.Select(x => x.Field);

                Assert.Contains(keys, "VendorTxCode");
                Assert.Contains(keys, "Amount");
                Assert.Contains(keys, "Description");
                Assert.Contains(keys, "RelatedVPSTxId");
                Assert.Contains(keys, "RelatedVendorTxCode");
                Assert.Contains(keys, "RelatedSecurityKey");
                Assert.Contains(keys, "RelatedTxAuthNo");
            }

            [Test]
            public void AmountPositive()
            {
                var request = SampleRequest();
                request.Amount = -40;
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual(errors.First().Field, "Amount");
            }

            [Test]
            public void TxAuthNumberPositive()
            {
                var request = SampleRequest();
                request.RelatedTxAuthNo = -40;
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual(errors.First().Field, "RelatedTxAuthNo");
            }
        }

        public static SagePay.Request.Refund.RefundRequest SampleRequest()
        {
            var request = new SagePay.Request.Refund.RefundRequest();

            request.VendorTxCode = Guid.NewGuid().ToString();
            request.Amount = 100.23m;
            request.Currency = SagePay.Request.Currency.GBP;
            request.Description = "Sample Refund";

            request.RelatedVPSTxId = "A1234";
            request.RelatedVendorTxCode = Guid.NewGuid().ToString();
            request.RelatedSecurityKey = "A123456789";
            request.RelatedTxAuthNo = 123456123;

            return request;

        }
    }
}
