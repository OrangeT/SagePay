using System.Linq;
using Xunit;
using OrangeTentacle.SagePay;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Configuration;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    
    public class OfflineSagePayment
    {
        
        public class Constructor
        {
            [Fact]
            public void ConfigureFromConfig()
            {
                var request = new SagePay.Request.Payment.OfflineSagePayment();
                var section = SagePay.Configuration.SageConfiguration.GetSection(ProviderTypes.Offline);


                Assert.Equal(section.VendorName, request.Vendor.VendorName);
            }

            [Fact]
            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new SagePay.Request.Payment.OfflineSagePayment(name);

                Assert.Equal(name, request.Vendor.VendorName);
            }
        }

        
        public class Validate
        {
            [Fact]
            public void ReturnsErrors()
            {
                var request = new SagePay.Request.Payment.OfflineSagePayment();
                request.Transaction = PaymentRequest.SampleRequest();
                request.Transaction.CV2 = "12";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CV2", errors.First().Field);
                Assert.False(request.IsValid);
            }

            [Fact]
            public void SetsIsValid()
            {
                var request = new SagePay.Request.Payment.OfflineSagePayment();
                request.Transaction = PaymentRequest.SampleRequest();
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
                var request = new SagePay.Request.Payment.OfflineSagePayment();

                Assert.Throws<SageException>(() => request.Send());
            }

            [Fact]
            public void EmitsAResponseIsValid()
            {
                var request = new SagePay.Request.Payment.OfflineSagePayment();
                request.Transaction = PaymentRequest.SampleRequest();
                request.Validate();

                var response = request.Send();
                Assert.NotNull(response);
            }
        }
    }
}

