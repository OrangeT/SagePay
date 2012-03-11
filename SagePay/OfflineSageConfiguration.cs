using System.Configuration;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageConfiguration : ConfigurationSection
    {
        private const string sectionName = "OfflineSageConfiguration";

        public static OfflineSageConfiguration GetSection()
        {
            return ConfigurationManager.GetSection(sectionName) as OfflineSageConfiguration;
        }

        [ConfigurationProperty("vendorName", IsRequired = true)]
        public string VendorName
        {
            get { return (string)this["vendorName"]; }
        }
    }
}