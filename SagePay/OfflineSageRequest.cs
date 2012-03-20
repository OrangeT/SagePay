using System;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageRequest : SageRequest
    {
        public OfflineSageRequest() 
            : base(SagePayFactory.ProviderTypes.Offline)
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
