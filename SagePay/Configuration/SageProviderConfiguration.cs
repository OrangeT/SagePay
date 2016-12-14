using System.Configuration;

namespace OrangeTentacle.SagePay.Configuration
{
    public class SageProviderConfiguration: ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public ProviderTypes Type
        {
            get
            {
                return (ProviderTypes) this["type"];
            }
        }

        [ConfigurationProperty("vendorName", IsRequired = true)]
        public string VendorName
        {
            get { return (string)this["vendorName"]; }
        }

        [ConfigurationProperty("encodeKey", IsRequired = false)]
        public string EncodeKey
        {
            get { return (string)this["encodeKey"]; }
        }
    }
}
