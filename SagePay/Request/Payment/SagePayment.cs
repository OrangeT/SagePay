using System.Collections.Generic;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public abstract class SagePayment : BaseSageRequest
    {
        public PaymentRequest Transaction { get; set; }

        public SagePayment(ProviderTypes type)
            : base(type)
        {}

        public SagePayment(string vendorName)
            : base(vendorName)
        {}

        public List<ValidationError> Validate()
        {
            return Validate(Transaction, out _isValid);
        }

        public abstract PaymentResponse Send();
    }


}