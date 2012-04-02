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
                return FetchForConfiguration(type, GetSection(null));
            }

            public static SageRequest Fetch(string filename)
            {
                return Fetch((ProviderTypes?)null, filename);
            }

            public static SageRequest Fetch(ProviderTypes? type, string filename)
            {
                return FetchForConfiguration(type, GetSection(filename));
            }

            private static SageRequest FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
            {
                var provider = section.Providers[type ?? section.Default];
                return Fetch(provider.Type, provider.VendorName);
            }

            public static SageRequest Fetch(ProviderTypes type, string vendorName)
            {
                var paymentType = Map.Single(x => x.ProviderType == type).PaymentType;
                return (SageRequest)Activator.CreateInstance(paymentType);
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
                return FetchForConfiguration(type, GetSection(null));
            }

            public static SageRefundRequest Fetch(string filename)
            {
                return Fetch((ProviderTypes?)null, filename);
            }

            public static SageRefundRequest Fetch(ProviderTypes? type, string filename)
            {
                return FetchForConfiguration(type, GetSection(filename));
            }

            private static SageRefundRequest FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
            {
                var provider = section.Providers[type ?? section.Default];
                return Fetch(provider.Type, provider.VendorName);
            }

            public static SageRefundRequest Fetch(ProviderTypes type, string vendorName)
            {
                var paymentType = Map.Single(x => x.ProviderType == type).RefundType;
                return (SageRefundRequest)Activator.CreateInstance(paymentType);
            }
        }

        internal class TypeMap
        {
            public ProviderTypes ProviderType { get; set; }
            public Type PaymentType { get; set; }
            public Type RefundType { get; set; }

            public TypeMap(ProviderTypes type, Type paymentType, Type refundType)
            {
                ProviderType = type;
                PaymentType = paymentType;
                RefundType = refundType;
            }
        }

        internal static readonly TypeMap[] Map = new TypeMap[] {
            new TypeMap(ProviderTypes.Live, typeof(LiveSageRequest), typeof(LiveSageRefund)),
            new TypeMap(ProviderTypes.Test, typeof(TestSageRequest), typeof(TestSageRefund)),
            new TypeMap(ProviderTypes.Simulator, typeof(SimulatorSageRequest), typeof(SimulatorSageRefund)),
            new TypeMap(ProviderTypes.Offline, typeof(OfflineSageRequest), typeof(OfflineSageRefund))
        };

        internal static SageConfiguration GetSection(string filename)
        {
            if (filename == null)
                return ConfigurationManager.GetSection(SageConfiguration.sectionName) as SageConfiguration;

            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = filename;
            var manager = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return manager.GetSection(SageConfiguration.sectionName) as SageConfiguration;
        }
    }
}
