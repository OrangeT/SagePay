using System.Configuration;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageRequest
    {
        public VendorConfiguration Vendor { get; private set; } 

        public OfflineSageRequest()
        {
            var config = OfflineSageConfiguration.GetSection();
            Vendor = new VendorConfiguration(config.VendorName);
        }

        public OfflineSageRequest(string name)
        {
            Vendor = new VendorConfiguration(name);
        }

        //public object Send()
        //{
        //    return "";
        //}

        public class VendorConfiguration
        {
            public string VendorName { get; private set; }

            public VendorConfiguration(string vendorName)
            {
                if (string.IsNullOrWhiteSpace(vendorName))
                    throw new ConfigurationErrorsException("Vendor Must Have VendorName");

                VendorName = vendorName;
            }
        }
    }

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
