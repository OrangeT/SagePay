using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class OfflineSageConfiguration
    {
        [TestFixture]
        internal class VendorName
        {
            [Test]
            public void FromConfigFile()
            {
                var section = SagePay.OfflineSageConfiguration.GetSection();
                Assert.IsFalse(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}