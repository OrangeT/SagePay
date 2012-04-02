using System;
using OrangeTentacle.SagePay;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Refund
{
    public class OfflineSageRefund : SageRefundRequest
    {
        public OfflineSageRefund() : base(ProviderTypes.Offline)
        {}

        public OfflineSageRefund(string vendorName) : base(vendorName)
        {}

        public override RefundResponse Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");
            return new RefundResponse();
        }
    }
}