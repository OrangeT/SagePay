using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OrangeTentacle.SagePay
{
    public class SagePayFactory
    {
        public static SageRequest Fetch()
        {
            return Fetch((ProviderTypes?)null);
        }

        public static SageRequest Fetch(ProviderTypes? type)
        {
            var section = ConfigurationManager.GetSection(SageConfiguration.sectionName) as SageConfiguration;
            return FetchForConfiguration(type, section);
        }

        public static SageRequest Fetch(string filename)
        {
            return Fetch((ProviderTypes?)null, filename);
        }

        public static SageRequest Fetch(ProviderTypes? type, string filename)
        {
            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = filename;
            var manager = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var section = manager.GetSection(SageConfiguration.sectionName) as SageConfiguration;
            return FetchForConfiguration(type, section);
        }

        private static SageRequest FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
        {
            var provider = section.Providers[type ?? section.Default];
            return Fetch(provider.Type, provider.VendorName);
        }

        public static SageRequest Fetch(ProviderTypes type, string vendorName)
        {
            switch (type)
            {
                case ProviderTypes.Live:
                    return string.IsNullOrEmpty(vendorName) ? new LiveSageRequest() : new LiveSageRequest(vendorName);
                case ProviderTypes.Test:
                    return string.IsNullOrEmpty(vendorName) ? new TestSageRequest() : new TestSageRequest(vendorName);
                case ProviderTypes.Simulator:
                    return string.IsNullOrEmpty(vendorName) ? new SimulatorSageRequest() : new SimulatorSageRequest(vendorName);
                default:
                    return string.IsNullOrEmpty(vendorName) ? new OfflineSageRequest() : new OfflineSageRequest(vendorName);
            }
        }

        public enum ProviderTypes
        {
            Live,
            Test,
            Simulator,
            Offline
        }
    }
}
