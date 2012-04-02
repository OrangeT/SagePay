using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrangeTentacle.SagePay;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Request.Refund;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Refund
{
    public abstract class SageRefundRequest : BaseSageRequest
    {
        public RefundRequest Transaction { get; set; }

        public SageRefundRequest(ProviderTypes type)
            : base(type)
        { }

        public SageRefundRequest(string vendorName)
            : base(vendorName)
        { }

        public List<ValidationError> Validate()
        {
            return Validate(Transaction, out _isValid);
        }

        public abstract RefundResponse Send();
    }
}
