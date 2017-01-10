using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using OrangeTentacle.SagePay.Configuration;
using OrangeTentacle.SagePay.Forms;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePay
{
    public static class SagePayFactory
    {
        public static class Form
        {
            public static SageForm Fetch()
            {
                return Fetch((ProviderTypes?)null);
            }

            public static SageForm Fetch(ProviderTypes? type)
            {
                return FetchForConfiguration(type, GetSection(null));
            }

            public static SageForm Fetch(string filename)
            {
                return Fetch((ProviderTypes?)null, filename);
            }

            public static SageForm Fetch(ProviderTypes? type, string filename)
            {
                return FetchForConfiguration(type, GetSection(filename));
            }

            private static SageForm FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
            {
                var provider = section.Providers[type ?? section.Default];
                return Fetch(provider.Type, provider.VendorName);
            }

            public static SageForm Fetch(ProviderTypes type, string vendorName)
            {
                var formType = Map.Single(x => x.ProviderType == type).FormType;
                return (SageForm)Activator.CreateInstance(formType);
            }

        }

        public static class Payment
        {

            public static SagePayment Fetch()
            {
                return Fetch((ProviderTypes?)null);
            }

            public static SagePayment Fetch(ProviderTypes? type)
            {
                return FetchForConfiguration(type, GetSection(null));
            }

            public static SagePayment Fetch(string filename)
            {
                return Fetch((ProviderTypes?)null, filename);
            }

            public static SagePayment Fetch(ProviderTypes? type, string filename)
            {
                return FetchForConfiguration(type, GetSection(filename));
            }

            private static SagePayment FetchForConfiguration(ProviderTypes? type, SageConfiguration section)
            {
                var provider = section.Providers[type ?? section.Default];
                return Fetch(provider.Type, provider.VendorName);
            }

            public static SagePayment Fetch(ProviderTypes type, string vendorName)
            {
                var paymentType = Map.Single(x => x.ProviderType == type).PaymentType;
                return (SagePayment)Activator.CreateInstance(paymentType);
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
            public Type FormType { get; set; }

            public TypeMap(ProviderTypes type, Type paymentType, Type refundType, Type formType)
            {
                ProviderType = type;
                PaymentType = paymentType;
                RefundType = refundType;
                FormType = formType;
            }
        }

        internal static readonly TypeMap[] Map = new TypeMap[] {
            new TypeMap(ProviderTypes.Live, typeof(LiveSagePayment), typeof(LiveSageRefund), typeof(LiveSageForm)),
            new TypeMap(ProviderTypes.Test, typeof(TestSagePayment), typeof(TestSageRefund), typeof(TestSageForm)),
            new TypeMap(ProviderTypes.Simulator, typeof(SimulatorSagePayment), typeof(SimulatorSageRefund), null),
            new TypeMap(ProviderTypes.Offline, typeof(OfflineSagePayment), typeof(OfflineSageRefund), null)
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
