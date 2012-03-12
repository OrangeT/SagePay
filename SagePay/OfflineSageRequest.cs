using System;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageRequest : SageRequest
    {
        public OfflineSageRequest() 
            : base("OfflineSageConfiguration")
        {
        }

        public OfflineSageRequest(string vendorName)
            : base(vendorName, true)
        {}

        public override TransactionResponse Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");
            return new TransactionResponse();
        }
    }
}
