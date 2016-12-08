using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    
    public class OfflineSageRefund
    {
        
        public class Constructor
        {
            [Fact]
            public void ConfigureFromConfig()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                var section = SagePay.Configuration.SageConfiguration.GetSection(ProviderTypes.Offline);

                Assert.Equal(section.VendorName, request.Vendor.VendorName);
            }

            [Fact]
            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new SagePay.Request.Refund.OfflineSageRefund(name);

                Assert.Equal(name, request.Vendor.VendorName);
            }
        }

        
        public class Validate
        {
            [Fact]
            public void ReturnsErrors()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = new SagePay.Request.Refund.RefundRequest();
                var errors = request.Validate();

                Assert.NotEmpty(errors);
            }

            [Fact]
            public void SetsIsValid()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = RefundRequest.SampleRequest();
                var errors = request.Validate();

                Assert.Equal(0, errors.Count);
                Assert.True(request.IsValid);
            }
        }

        
        public class Send
        {
            [Fact]
            public void ThInlineDatasAnExceptionIfInvalid()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();

                Assert.Throws<SageException>(() =>  request.Send());
            }

            [Fact]
            public void EmitsAResponseIfValid()
            {
                var request = new SagePay.Request.Refund.OfflineSageRefund();
                request.Transaction = RefundRequest.SampleRequest();
                request.Validate();

                var response = request.Send();
                Assert.NotNull(response);
            }
        }
    }
}
