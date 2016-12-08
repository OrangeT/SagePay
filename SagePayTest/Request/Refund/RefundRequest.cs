using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    class RefundRequest
    {
        
        public class Validate
        {
            [Fact]
            public void AllRequiredFields()
            {
                var request = SampleRequest();
                var errors = request.Validate();

                Assert.Equal(0, errors.Count);
            }

            [Fact]
            public void MissingFields()
            {
                var request = new SagePay.Request.Refund.RefundRequest();
                var errors = request.Validate();

                var keys = errors.Select(x => x.Field).ToList();

                Assert.Contains("VendorTxCode", keys);
                Assert.Contains("Amount", keys);
                Assert.Contains("Description", keys);
                Assert.Contains("RelatedVPSTxId", keys);
                Assert.Contains("RelatedVendorTxCode", keys);
                Assert.Contains("RelatedSecurityKey", keys);
                Assert.Contains("RelatedTxAuthNo", keys);
            }

            [Fact]
            public void AmountPositive()
            {
                var request = SampleRequest();
                request.Amount = -40;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal(errors.First().Field, "Amount");
            }

            [Fact]
            public void TxAuthNumberPositive()
            {
                var request = SampleRequest();
                request.RelatedTxAuthNo = -40;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal(errors.First().Field, "RelatedTxAuthNo");
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
