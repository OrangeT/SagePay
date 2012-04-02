using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePay.Request.Payment
{
    public class WebSageRequest : SageRequest
    {
        public string Url { get; protected set; }

        public WebSageRequest(ProviderTypes type, string url)
            : base(type)
        {
            Url = url;
        }

        public WebSageRequest(string vendorName, string url, bool filler)
            : base (vendorName)
        {
            Url = url;
        }

        public NameValueCollection Encode()
        {
            var collection = new NameValueCollection();

            collection.Add("VPSProtocol", Transaction.VPSProtocol);

            if (Transaction.TxType == TransactionRequest.PaymentType.AuthenticateOnly)
                collection.Add("TxType", "AUTHENTICATE ONLY");
            if (Transaction.TxType == TransactionRequest.PaymentType.Deferred)
                collection.Add("TxType", "DEFERRED");
            if (Transaction.TxType == TransactionRequest.PaymentType.Payment)
                collection.Add("TxType", "PAYMENT");

            collection.Add("Vendor", Vendor.VendorName);
            collection.Add("VendorTxCode", Transaction.VendorTxCode);
            collection.Add("Amount", Transaction.Amount.ToString());
            collection.Add("Currency", Transaction.Currency.ToString().ToUpper());
            collection.Add("Description", Transaction.Description);
            collection.Add("CardHolder", Transaction.CardHolderName);
            collection.Add("CardNumber", Transaction.CardNumber);
            collection.Add("ExpiryDate", Transaction.ExpiryDate.ToString("MMyy"));
            collection.Add("CV2", Transaction.CV2);
            collection.Add("CardType", Transaction.CardType.ToString());
            collection.Add("BillingSurname", Transaction.Billing.Surname);
            collection.Add("BillingFirstnames", Transaction.Billing.Firstnames);
            collection.Add("BillingAddress1", Transaction.Billing.Address1);
            collection.Add("BillingCity", Transaction.Billing.City);
            collection.Add("BillingPostCode", Transaction.Billing.PostCode);
            collection.Add("BillingCountry", Transaction.Billing.Country);
            collection.Add("DeliverySurname", Transaction.Delivery.Surname);
            collection.Add("DeliveryFirstnames", Transaction.Delivery.Firstnames);
            collection.Add("DeliveryAddress1", Transaction.Delivery.Address1);
            collection.Add("DeliveryCity", Transaction.Delivery.City);
            collection.Add("DeliveryPostCode", Transaction.Delivery.PostCode);
            collection.Add("DeliveryCountry", Transaction.Delivery.Country);
            return collection;
        }

        public static TransactionResponse Decode(string input)
        {
            var response = new TransactionResponse();

            var lines = input.Split('\n');
            var collection = new Dictionary<string, string>();

            foreach(var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split('=');
                collection.Add(values[0].Trim(), values[1].Trim());
            }

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

        public override TransactionResponse Send()
        {
            if (!IsValid)
                throw new SageException("Configuration Must Be Valid");

            return Decode(WebHelper.SendRequest(Url, Encode()));
        }
    }
}