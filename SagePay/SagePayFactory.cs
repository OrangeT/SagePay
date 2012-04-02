using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using OrangeTentacle.SagePay.Configuration;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePay
{
    public static class SagePayFactory
    {
        public static class Payment
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
        }

        public static class Refund
        {
            public static SageRefundRequest Fetch()
            {
                return Fetch((ProviderTypes?)null);
            }

            public static SageRefundRequest Fetch(ProviderTypes? type)
            {
                var section = ConfigurationManager.GetSection(SageConfiguration.sectionName) as SageConfiguration;
                return FetchForConfiguration(type, section);
            }

            public static SageRefundRequest Fetch(string filename)
            {
                return Fetch((ProviderTypes?)null, filename);
            }

            public static SageRefundRequest Fetch(ProviderTypes? type, string filename)
            {
                var fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = filename;
                var manager = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var section = manager.GetSection(SageConfiguration.sectionName) as SageConfiguration;
                return FetchForConfiguration(type, section);
            }

            private static SageRefundRequest FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
            {
                var provider = section.Providers[type ?? section.Default];
                return Fetch(provider.Type, provider.VendorName);
            }

            public static SageRefundRequest Fetch(ProviderTypes type, string vendorName)
            {
                switch (type)
                {
                    case ProviderTypes.Live:
                        return string.IsNullOrEmpty(vendorName) ? new LiveSageRefund() : new LiveSageRefund(vendorName);
                    case ProviderTypes.Test:
                        return string.IsNullOrEmpty(vendorName) ? new TestSageRefund() : new TestSageRefund(vendorName);
                    case ProviderTypes.Simulator:
                        return string.IsNullOrEmpty(vendorName) ? new SimulatorSageRefund() : new SimulatorSageRefund(vendorName);
                    default:
                        return string.IsNullOrEmpty(vendorName) ? new OfflineSageRefund() : new OfflineSageRefund(vendorName);
                }
            }
        }
    }
}
