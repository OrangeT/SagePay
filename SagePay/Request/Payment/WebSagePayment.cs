using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public class WebSagePayment : SagePayment
    {
        public string Url { get; protected set; }

        public WebSagePayment(ProviderTypes type, string url)
            : base(type)
        {
            Url = url;
        }

        public WebSagePayment(string vendorName, string url, bool filler)
            : base (vendorName)
        {
            Url = url;
        }

        public NameValueCollection Encode()
        {
            var collection = new NameValueCollection();

            collection.Add("VPSProtocol", Payment.VPSProtocol);

            if (Payment.TxType == PaymentRequest.PaymentType.AuthenticateOnly)
                collection.Add("TxType", "AUTHENTICATE ONLY");
            if (Payment.TxType == PaymentRequest.PaymentType.Deferred)
                collection.Add("TxType", "DEFERRED");
            if (Payment.TxType == PaymentRequest.PaymentType.Payment)
                collection.Add("TxType", "PAYMENT");

            collection.Add("Vendor", Vendor.VendorName);
            collection.Add("VendorTxCode", Payment.VendorTxCode);
            collection.Add("Amount", Payment.Amount.ToString());
            collection.Add("Currency", Payment.Currency.ToString().ToUpper());
            collection.Add("Description", Payment.Description);
            collection.Add("CardHolder", Payment.CardHolderName);
            collection.Add("CardNumber", Payment.CardNumber);
            collection.Add("ExpiryDate", Payment.ExpiryDate.ToString("MMyy"));
            collection.Add("CV2", Payment.CV2);
            collection.Add("CardType", Payment.CardType.ToString());
            collection.Add("BillingSurname", Payment.Billing.Surname);
            collection.Add("BillingFirstnames", Payment.Billing.Firstnames);
            collection.Add("BillingAddress1", Payment.Billing.Address1);
            collection.Add("BillingCity", Payment.Billing.City);
            collection.Add("BillingPostCode", Payment.Billing.PostCode);
            collection.Add("BillingCountry", Payment.Billing.Country);
            collection.Add("DeliverySurname", Payment.Delivery.Surname);
            collection.Add("DeliveryFirstnames", Payment.Delivery.Firstnames);
            collection.Add("DeliveryAddress1", Payment.Delivery.Address1);
            collection.Add("DeliveryCity", Payment.Delivery.City);
            collection.Add("DeliveryPostCode", Payment.Delivery.PostCode);
            collection.Add("DeliveryCountry", Payment.Delivery.Country);
            return collection;
        }

        public static PaymentResponse Decode(string input)
        {
            var response = new PaymentResponse();
            var collection = WebHelper.SplitResponse(input);

            response.Status = WebHelper.EnumFromString<ResponseStatus>(collection["Status"]);
            response.StatusDetail = collection["StatusDetail"];

            if (response.Status == ResponseStatus.Invalid ||
                response.Status == ResponseStatus.Error)
                return response;

            if (response.Status == ResponseStatus.OK)
                response.TxAuthNo = long.Parse(collection["TxAuthNo"]);

            if (response.Status != ResponseStatus.ThreeDAuth)
                response.VPSTxId = collection["VPSTxId"];

            if (response.Status != ResponseStatus.ThreeDAuth &&
                response.Status != ResponseStatus.Ppredirect)
                response.SecurityKey = collection["SecurityKey"];

            if (response.Status != ResponseStatus.ThreeDAuth &&
                response.Status != ResponseStatus.Authenticated &&
                response.Status != ResponseStatus.Registered &&
                response.Status != ResponseStatus.Ppredirect)
            {

                response.AVSCV2 = WebHelper.EnumFromString<CV2Status>(collection["AVSCV2"]);
                response.AddressResult = WebHelper.EnumFromString<MatchStatus>(collection["AddressResult"]);
                response.PostCodeResult = WebHelper.EnumFromString<MatchStatus>(collection["PostCodeResult"]);
                response.CV2Result = WebHelper.EnumFromString<MatchStatus>(collection["CV2Result"]);
            }

            // Doc state that if not enabled, should return "NOTCHECKED"
            // Nothing being returned from simulator - therefore default response.
            response.ThreeDSecure = ThreeDSecureStatus.NotChecked;
            if (collection.ContainsKey("3DSecureStatus"))
                response.ThreeDSecure = WebHelper.EnumFromString<ThreeDSecureStatus>(collection["3DSecureStatus"]);

            if (response.ThreeDSecure == ThreeDSecureStatus.OK &&
                response.Status == ResponseStatus.OK)
                response.Caav = collection["CAVV"];

            return response;

        }

        public override PaymentResponse Send()
        {
            if (!IsValid)
                throw new SageException("Configuration Must Be Valid");

            return Decode(WebHelper.SendRequest(Url, Encode()));
        }
    }
}