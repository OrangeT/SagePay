using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class SageConfiguration
    {
        public const string CONFIG_SECTION = "OfflineSagePay";

        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = SagePay.SageConfiguration.GetSection(CONFIG_SECTION);
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}