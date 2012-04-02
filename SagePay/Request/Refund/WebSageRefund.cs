using System;
using System.Collections.Specialized;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Refund
{
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
            var response = new RefundResponse();
            var collection = WebHelper.SplitResponse(input);

            response.Status = WebHelper.EnumFromString<ResponseStatus>(collection["Status"]);
            response.StatusDetail = collection["StatusDetail"];

            if (response.Status != ResponseStatus.OK)
                return response;

            response.VPSTxId = collection["VPSTxId"];
            response.TxAuthNo = long.Parse(collection["TxAuthNo"]);

            return response;
        }

        public override RefundResponse Send()
        {
            if (!IsValid)
                throw new SageException("Configuration Must Be Valid");

            return Decode(WebHelper.SendRequest(Url, Encode()));
        }
    }
}