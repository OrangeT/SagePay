using System;
using System.Configuration;
using OrangeTentacle.SagePay.Request;

namespace OrangeTentacle.SagePay.Configuration
{
    public class SageConfiguration : ConfigurationSection
    {
        public const string sectionName = "SagePay";

        public static SageProviderConfiguration GetSection(ProviderTypes providerType)
        {
            var section = ConfigurationManager.GetSection(sectionName) as SageConfiguration;
            return section.Providers[providerType];
        }

        public ProviderTypes Default
        {
            get { return InternalDefault.Value; }
        }

        [ConfigurationProperty("default", IsRequired = false)]
        public ProviderTypes? InternalDefault
        {
            get
            {
                if (this["default"] != null)
                    return (ProviderTypes)this["default"];

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
}