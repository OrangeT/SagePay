using System;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public class OfflineSagePayment : SagePayment
    {
        public OfflineSagePayment() 
            : base(ProviderTypes.Offline)
        {
        }

        public OfflineSagePayment(string vendorName)
            : base(vendorName)
        {}

        public override PaymentResponse Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");
            return new PaymentResponse();
        }
    }
}
