using MbUnit.Framework;
using System.Configuration;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class OfflineSageRequest
    {
        [TestFixture]
        internal class Configuration
        {
            [Test]
            public void ConfigureFromConfig()
            {
                var request = new OrangeTentacle.SagePay.OfflineSageRequest();
                var section = OrangeTentacle.SagePay.OfflineSageConfiguration.GetSection();


                Assert.AreEqual(section.VendorName, request.Vendor.VendorName);
            }

            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new OrangeTentacle.SagePay.OfflineSageRequest(name);
                
                Assert.AreEqual(name, request.Vendor.VendorName);
            }
        }

        [TestFixture]
        internal class VendorConfiguration
        {
            [Test]
            public void Constructor()
            {
                var name = "bob";
                var vendor = new OrangeTentacle.SagePay.OfflineSageRequest.VendorConfiguration(name);
                Assert.AreEqual(vendor.VendorName, name);
            }

            [Test]
            [AssertException(typeof(ConfigurationErrorsException))]
            public void ConstructorEmpty()
            {
                var name = string.Empty;
                var vendor = new OrangeTentacle.SagePay.OfflineSageRequest.VendorConfiguration(name);
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
    public class OfflineSageConfiguration
    {
        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = OrangeTentacle.SagePay.OfflineSageConfiguration.GetSection();
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}
