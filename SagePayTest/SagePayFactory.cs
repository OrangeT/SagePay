using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class SagePayFactory
    {
        [TestFixture]
        internal class Payment
        {
            [TestFixture]
            internal class Fetch
            {
                [Test]
                public void Default()
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch();
                    Assert.IsInstanceOfType<OfflineSagePayment>(provider);
                }

                [Test]
                [Factory(typeof (PathsAndTypes), "GetProviders")]
                public void ForProviderTypes(PathsAndTypes.ProviderMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch(mapping.ProviderType);
                    Assert.IsInstanceOfType(mapping.PaymentType, provider,
                                            string.Format("Failed to return Type {0} for ProviderType {1}",
                                                          mapping.PaymentType, mapping.ProviderType));
                }

                [Test]
                [Factory(typeof (PathsAndTypes), "GetFiles")]
                public void ForConfigFiles(PathsAndTypes.PathMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch(mapping.FileName);
                    Assert.IsInstanceOfType(mapping.PaymentType, provider);
                }
            }
        }

        [TestFixture]
        internal class Refund
        {
            [TestFixture]
            internal class Fetch
            {
                [Test]
                public void Default()
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch();
                    Assert.IsInstanceOfType<OfflineSageRefund>(provider);
                }

                [Test]
                [Factory(typeof(PathsAndTypes), "GetProviders")]
                public void ForProviderTypes(PathsAndTypes.ProviderMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch(mapping.ProviderType);
                    Assert.IsInstanceOfType(mapping.RefundType, provider,
                                            string.Format("Failed to return Type {0} for ProviderType {1}",
                                                          mapping.RefundType, mapping.ProviderType));
                }

                [Test]
                [Factory(typeof(PathsAndTypes), "GetFiles")]
                public void ForConfigFiles(PathsAndTypes.PathMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch(mapping.FileName);
                    Assert.IsInstanceOfType(mapping.RefundType, provider);
                }
            }
        }
    }
}
