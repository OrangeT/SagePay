using System.Collections.Generic;

namespace OrangeTentacle.SagePay
{
    public abstract class SageRequest : BaseSageRequest
    {
        public TransactionRequest Transaction { get; set; }

        public SageRequest(SagePayFactory.ProviderTypes type)
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

    //public abstract class SageRefundRequest : BaseSageRequest
    //{
    //    public RefundRequest Transaction { get; set; }

    //    public SageRefundRequest(SagePayFactory.ProviderTypes type)
    //        : base(type)
    //    {}

    //    public SageRefundRequest(string vendorName)
    //        : base(vendorName)
    //    {}

    //    public List<ValidationError> Validate()
    //    {
    //        return Validate(Transaction, out _isValid);
    //    }

    //    public abstract RefundResponse Send();
    //}
}