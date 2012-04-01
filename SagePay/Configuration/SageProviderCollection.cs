using System.Configuration;

namespace OrangeTentacle.SagePay.Configuration
{
    [ConfigurationCollection(typeof(SageProviderConfiguration), 
        CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class SageProviderCollection : ConfigurationElementCollection
    {
        public SageProviderConfiguration this[int index]
        {
            get { return (SageProviderConfiguration) BaseGet(index); }
        }

        public SageProviderConfiguration this[ProviderTypes index]
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
}