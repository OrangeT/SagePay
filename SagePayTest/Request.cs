using MbUnit.Framework;
using System.Configuration;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class OfflineSageRequestTest
    {
        [TestFixture]
        internal class Configuration
        {
            [Test]
            public void ConfigureFromConfig()
            {
                var request = new OfflineSageRequest();
                var section = OfflineSageConfiguration.GetSection();


                Assert.AreEqual(section.VendorName, request.Vendor.VendorName);
            }

            public void ConfigureFromConstructor()
            {
                
            }
        }

        //[TestFixture]
        //internal class Send
        //{
        //    [Test]
        //    public void EmitsAResponse()
        //    {
        //        var request = new OfflineSageRequest();
        //        var response = request.Send();
        //    }
        //}

    }

    [TestFixture]
    public class OfflineSageConfigurationTest
    {
        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = OfflineSageConfiguration.GetSection();
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}
