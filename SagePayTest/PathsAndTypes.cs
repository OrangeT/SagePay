using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using OrangeTentacle.SagePay;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePayTest
{
    
    public class PathsAndTypes
    {

        public class PathMapping
        {
            public const string BASE_PATH = "../../Examples/";

            public string FileName { get; set; }
            public Type PaymentType { get; set; }
            public Type RefundType { get; set; }

            public PathMapping(string filename, Type paymentType, Type refundType)
            {
                FileName = BASE_PATH + filename;
                PaymentType = paymentType;
                RefundType = refundType;
            }
        }

        public class ProviderMapping
        {
            public ProviderTypes ProviderType { get; set; }
            public Type PaymentType { get; set; }
            public Type RefundType { get; set; }

            public ProviderMapping(ProviderTypes type, Type paymentType, Type refundType)
            {
                ProviderType = type;
                PaymentType = paymentType;
                RefundType = refundType;
            }
        }

        public static IEnumerable<object[]> GetFiles()
        {
            var list = new List<object[]>();
            list.Add(new object[] { new PathMapping("App.config.default", typeof (OfflineSagePayment), typeof (OfflineSageRefund)) });
            list.Add(new object[] { new PathMapping("App.config.live", typeof (LiveSagePayment), typeof (LiveSageRefund)) });
            list.Add(new object[] { new PathMapping("App.config.test", typeof (TestSagePayment), typeof (TestSageRefund)) });
            list.Add(new object[] { new PathMapping("App.config.simulator", typeof (SimulatorSagePayment), typeof (SimulatorSageRefund)) });
            list.Add(new object[] { new PathMapping("App.config.offline", typeof (OfflineSagePayment), typeof (OfflineSageRefund))} );
            return list;
        }


        public static IEnumerable<object[]> GetProviders()
        {
            var list = new List<object[]>();
            list.Add(new object[] { new ProviderMapping(ProviderTypes.Live, typeof(LiveSagePayment), typeof(LiveSageRefund)) });
            list.Add(new object[] { new ProviderMapping(ProviderTypes.Test, typeof(TestSagePayment), typeof(TestSageRefund)) });
            list.Add(new object[] { new ProviderMapping(ProviderTypes.Simulator, typeof(SimulatorSagePayment), typeof(SimulatorSageRefund)) });
            list.Add(new object[] { new ProviderMapping(ProviderTypes.Offline, typeof(OfflineSagePayment), typeof(OfflineSageRefund)) });
            return list;
        }

        [Theory, MemberData("GetFiles")]
        public void TestFilesExist(PathMapping mapping)
        {
            Assert.True(File.Exists(mapping.FileName));
        }
    }
}