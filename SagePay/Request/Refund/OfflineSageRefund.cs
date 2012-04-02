using System;
using System.Collections.Specialized;
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

    public class WebSageRefund : SageRefundRequest
    {
        public string Url { get; protected set; }

        public WebSageRefund(ProviderTypes type, string url)
            : base(type)
        {
            Url = url;
        }

        public WebSageRefund(string vendorName, string url)
            : base (vendorName)
        {
            Url = url;
        }

        public NameValueCollection Encode()
        {
            var collection = new NameValueCollection();

            collection.Add("VPSProtocol", Transaction.VPSProtocol);
            collection.Add("TxType", "REFUND");
            collection.Add("Vendor", Vendor.VendorName);
            collection.Add("VendorTxCode", Transaction.VendorTxCode);
            collection.Add("Amount", Transaction.Amount.ToString());
            collection.Add("Currency", Transaction.Currency.ToString().ToUpper());
            collection.Add("Description", Transaction.Description);
            collection.Add("RelatedVPSTxId", Transaction.RelatedVPSTxId);
            collection.Add("RelatedVendorTxCode", Transaction.RelatedVendorTxCode);
            collection.Add("RelatedSecurityKey", Transaction.RelatedSecurityKey);
            collection.Add("RelatedTxAuthNo", Transaction.RelatedTxAuthNo.ToString());

            return collection;
        }

        public static RefundResponse Decode(string input)
        {
            throw new NotImplementedException();
        }

        public override RefundResponse Send()
        {
            if (!IsValid)
                throw new SageException("Configuration Must Be Valid");

            return Decode(WebHelper.SendRequest(Url, Encode()));
        }
    }
}