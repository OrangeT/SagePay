using System.Configuration;
using Xunit;
using OrangeTentacle.SagePayTest.Configuration;

namespace OrangeTentacle.SagePayTest.Request
{
    
    public class VendorRequest
    {
        
        public class Constructor
        {

            [Fact]
            public void Valid()
            {
                var name = "bob";
                var vendor = new SagePay.Request.VendorRequest(name);
                Assert.Equal(vendor.VendorName, name);
            }

            [Fact]
            public void EmptyIsNotValid()
            {
                var name = string.Empty;

                Assert.Throws<ConfigurationErrorsException>(() => new SagePay.Request.VendorRequest(name));
            }
        }

        public static SagePay.Request.VendorRequest SampleRequest()
        {
            var name = SagePay.Configuration.SageConfiguration.GetSection(SageConfiguration.CONFIG_TYPE).VendorName;
            var request = new SagePay.Request.VendorRequest(name);

            return request;
        }
    }
}