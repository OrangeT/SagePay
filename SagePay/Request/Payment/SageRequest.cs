using System.Collections.Generic;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public abstract class SageRequest : BaseSageRequest
    {
        public TransactionRequest Transaction { get; set; }

        public SageRequest(ProviderTypes type)
            : base(type)
        {}

        public SageRequest(string vendorName)
            : base(vendorName)
        {}

        public List<ValidationError> Validate()
        {
            return Validate(Transaction, out _isValid);
        }

        public abstract TransactionResponse Send();
    }


}