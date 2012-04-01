using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest.Configuration
{
    [TestFixture]
    public class SageConfiguration
    {
        public const ProviderTypes CONFIG_TYPE = ProviderTypes.Offline;

        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = SagePay.Configuration.SageConfiguration.GetSection(CONFIG_TYPE);
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}