using System.Configuration;
using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class VendorRequest
    {
        [TestFixture]
        internal class Constructor
        {

            [Test]
            public void Valid()
            {
                var name = "bob";
                var vendor = new SagePay.Request.VendorRequest(name);
                Assert.AreEqual(vendor.VendorName, name);
            }

            [Test]
            [AssertException(typeof(ConfigurationErrorsException))]
            public void EmptyIsNotValid()
            {
                var name = string.Empty;
                var vendor = new SagePay.Request.VendorRequest(name);
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