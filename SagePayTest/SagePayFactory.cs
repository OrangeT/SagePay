using System;
using System.IO;
using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class SagePayFactory
    {
        [TestFixture]
        internal class Fetch
        {
            [Test]
            public void Live()
            {
                var provider = SagePay.SagePayFactory.Fetch(SagePay.SagePayFactory.ProviderTypes.Live);

                Assert.IsInstanceOfType(typeof(SagePay.LiveSageRequest), provider);
            }

            [Test]
            public void Test()
            {
                var provider = SagePay.SagePayFactory.Fetch(SagePay.SagePayFactory.ProviderTypes.Test);
                Assert.IsInstanceOfType(typeof(SagePay.TestSageRequest), provider);
            }

            [Test]
            public void Simulator()
            {
                var provider = SagePay.SagePayFactory.Fetch(SagePay.SagePayFactory.ProviderTypes.Simulator);
                Assert.IsInstanceOfType(typeof(SagePay.SimulatorSageRequest), provider);
            }

            [Test]
            public void Offline()
            {
                var provider = SagePay.SagePayFactory.Fetch(SagePay.SagePayFactory.ProviderTypes.Offline);
                Assert.IsInstanceOfType(typeof(SagePay.OfflineSageRequest), provider);
            }
        }

        [TestFixture]
        internal class FetchFromConfig
        {
            public const string BASE_PATH = "../../Examples/";
            public const string DEFAULT_FILE = BASE_PATH + "App.config.default";
            public const string LIVE_FILE = BASE_PATH + "App.config.live";
            public const string TEST_FILE = BASE_PATH + "App.config.test";
            public const string SIMULATOR_FILE = BASE_PATH + "App.config.simulator";
            public const string OFFLINE_FILE = BASE_PATH + "App.config.offline";

            // Load the relevant default provider from config.

            [Test]
            public void TestFilesExist()
            {
                Assert.IsTrue(File.Exists(DEFAULT_FILE));
                Assert.IsTrue(File.Exists(LIVE_FILE));
                Assert.IsTrue(File.Exists(TEST_FILE));
                Assert.IsTrue(File.Exists(SIMULATOR_FILE));
                Assert.IsTrue(File.Exists(OFFLINE_FILE));
            }

            [Test]
            public void Default_Offline()
            {
                // Where multiple providers exist, and no default specified, 
                // return the first in the list
                var provider = SagePay.SagePayFactory.Fetch(DEFAULT_FILE);
                Assert.IsInstanceOfType(typeof(SagePay.OfflineSageRequest), provider);
            }

            [Test]
            public void Live()
            {
                var provider = SagePay.SagePayFactory.Fetch(LIVE_FILE);
                Assert.IsInstanceOfType(typeof(SagePay.LiveSageRequest), provider);
            }

            [Test]
            public void Test()
            {
                var provider = SagePay.SagePayFactory.Fetch(TEST_FILE);
                Assert.IsInstanceOfType(typeof(SagePay.TestSageRequest), provider);
            }

            [Test]
            public void Simulator()
            {
                var provider = SagePay.SagePayFactory.Fetch(SIMULATOR_FILE);
                Assert.IsInstanceOfType(typeof(SagePay.SimulatorSageRequest), provider);                
            }

            [Test]
            public void Offline()
            {
                var provider = SagePay.SagePayFactory.Fetch(OFFLINE_FILE);
                Assert.IsInstanceOfType(typeof(SagePay.OfflineSageRequest), provider);
            }
        }
    
    }
}
