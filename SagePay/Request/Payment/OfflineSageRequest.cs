using System;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public class OfflineSageRequest : SageRequest
    {
        public OfflineSageRequest() 
            : base(ProviderTypes.Offline)
        {
        }

        public OfflineSageRequest(string vendorName)
            : base(vendorName)
        {}

        public override TransactionResponse Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");
            return new TransactionResponse();
        }
    }
}
