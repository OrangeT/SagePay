using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class SageConfiguration
    {
        public const SagePay.SagePayFactory.ProviderTypes CONFIG_TYPE = SagePay.SagePayFactory.ProviderTypes.Offline;

        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = SagePay.SageConfiguration.GetSection(CONFIG_TYPE);
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}