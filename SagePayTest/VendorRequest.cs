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
                var vendor = new OrangeTentacle.SagePay.VendorRequest(name);
                Assert.AreEqual(vendor.VendorName, name);
            }

            [Test]
            [AssertException(typeof(ConfigurationErrorsException))]
            public void EmptyIsNotValid()
            {
                var name = string.Empty;
                var vendor = new OrangeTentacle.SagePay.VendorRequest(name);
            }
        }

        public static SagePay.VendorRequest SampleRequest()
        {
            var name = SagePay.SageConfiguration.GetSection(SageConfiguration.CONFIG_SECTION).VendorName;
            var request = new SagePay.VendorRequest(name);

            return request;
        }
    }
}