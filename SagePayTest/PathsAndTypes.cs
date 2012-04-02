using System;
using System.Collections.Generic;
using System.IO;
using MbUnit.Framework;
using OrangeTentacle.SagePay;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    internal class PathsAndTypes
    {

        internal class PathMapping
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

        internal class ProviderMapping
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

        public static List<PathMapping> GetFiles()
        {
            var list = new List<PathMapping>();
            list.Add(new PathMapping("App.config.default", typeof (OfflineSageRequest), typeof (OfflineSageRefund)));
            list.Add(new PathMapping("App.config.live", typeof (LiveSageRequest), typeof (LiveSageRefund)));
            list.Add(new PathMapping("App.config.test", typeof (TestSageRequest), typeof (TestSageRefund)));
            list.Add(new PathMapping("App.config.simulator", typeof (SimulatorSageRequest), typeof (SimulatorSageRefund)));
            list.Add(new PathMapping("App.config.offline", typeof (OfflineSageRequest), typeof (OfflineSageRefund)));
            return list;
        }


        public static List<ProviderMapping> GetProviders()
        {
            var list = new List<ProviderMapping>();
            list.Add(new ProviderMapping(ProviderTypes.Live, typeof(LiveSageRequest), typeof(LiveSageRefund)));
            list.Add(new ProviderMapping(ProviderTypes.Test, typeof(TestSageRequest), typeof(TestSageRefund)));
            list.Add(new ProviderMapping(ProviderTypes.Simulator, typeof(SimulatorSageRequest), typeof(SimulatorSageRefund)));
            list.Add(new ProviderMapping(ProviderTypes.Offline, typeof(OfflineSageRequest), typeof(OfflineSageRefund)));
            return list;
        }

        [Test]
        [Factory("FilepathsAndTypes")]
        public void TestFilesExist(PathMapping mapping)
        {
            Assert.IsTrue(File.Exists(mapping.FileName));
        }
    }
}