using Xunit;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest.Configuration
{
    
    public class SageConfiguration
    {
        public const ProviderTypes CONFIG_TYPE = ProviderTypes.Offline;

        
        public class VendorName
        {
            [Fact]
            public void FromConfigFile()
            {
                var section = SagePay.Configuration.SageConfiguration.GetSection(CONFIG_TYPE);
                Assert.False(string.IsNullOrWhiteSpace(section.VendorName));
            }
        }
    }
}