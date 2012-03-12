using System.Configuration;

namespace OrangeTentacle.SagePay
{
    public class SageConfiguration : ConfigurationSection
    {
        public static SageConfiguration GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as SageConfiguration;
        }

        [ConfigurationProperty("vendorName", IsRequired = true)]
        public string VendorName
        {
            get { return (string)this["vendorName"]; }
        }
    }
}