using System.Linq;
using System.Reflection;
using Xunit;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePay.Request.Refund;

namespace OrangeTentacle.SagePayTest
{
    
    public class SagePayFactory
    {
        
        public class Payment
        {
            
            public class Fetch
            {
                [Fact]
                public void Default()
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch();
                    Assert.IsType<OfflineSagePayment>(provider);
                }

                [Theory, MemberData("GetProviders", MemberType = typeof(PathsAndTypes))]
                public void ForProviderTypes(PathsAndTypes.ProviderMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch(mapping.ProviderType);
                    Assert.IsType(mapping.PaymentType, provider);
                }

                [Theory, MemberData("GetFiles", MemberType = typeof(PathsAndTypes))]
                public void ForConfigFiles(PathsAndTypes.PathMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Payment.Fetch(mapping.FileName);
                    Assert.IsType(mapping.PaymentType, provider);
                }
            }
        }

        
        public class Refund
        {
            
            public class Fetch
            {
                [Fact]
                public void Default()
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch();
                    Assert.IsType<OfflineSageRefund>(provider);
                }

                [Theory, MemberData("GetProviders", MemberType = typeof(PathsAndTypes))]
                public void ForProviderTypes(PathsAndTypes.ProviderMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch(mapping.ProviderType);
                    Assert.IsType(mapping.RefundType, provider);
                }

                [Theory, MemberData("GetFiles", MemberType = typeof(PathsAndTypes))]
                public void ForConfigFiles(PathsAndTypes.PathMapping mapping)
                {
                    var provider = SagePay.SagePayFactory.Refund.Fetch(mapping.FileName);
                    Assert.IsType(mapping.RefundType, provider);
                }
            }
        }
    }
}
