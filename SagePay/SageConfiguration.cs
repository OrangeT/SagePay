using System;
using System.Configuration;

namespace OrangeTentacle.SagePay
{
    public class SageConfiguration : ConfigurationSection
    {
        public const string sectionName = "SagePay";

        public static SageProviderConfiguration GetSection(SagePayFactory.ProviderTypes providerType)
        {
            var section = ConfigurationManager.GetSection(sectionName) as SageConfiguration;
            return section.Providers[providerType];
        }

        public SagePayFactory.ProviderTypes Default
        {
            get { return InternalDefault.Value; }
        }

        [ConfigurationProperty("default", IsRequired = false)]
        public SagePayFactory.ProviderTypes? InternalDefault
        {
            get
            {
                if (this["default"] != null)
                    return (SagePayFactory.ProviderTypes)this["default"];

                if (Providers.Count > 0)
                    return Providers[0].Type;

                throw new SettingsPropertyNotFoundException("No providers specified");
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public SageProviderCollection Providers
        {
            get { return (SageProviderCollection) this[""]; }
        }

    }

    [ConfigurationCollection(typeof(SageProviderConfiguration), 
        CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class SageProviderCollection : ConfigurationElementCollection
    {
        public SageProviderConfiguration this[int index]
        {
            get { return (SageProviderConfiguration) BaseGet(index); }
        }

        public SageProviderConfiguration this[SagePayFactory.ProviderTypes index]
        {
            get { return (SageProviderConfiguration) BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SageProviderConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SageProviderConfiguration).Type;
        }
    }

    public class SageProviderConfiguration: ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public SagePayFactory.ProviderTypes Type
        {
            get
            {
                return (SagePayFactory.ProviderTypes) this["type"];
            }
        }

        [ConfigurationProperty("vendorName", IsRequired = true)]
        public string VendorName
        {
            get { return (string)this["vendorName"]; }
        }
    }
}