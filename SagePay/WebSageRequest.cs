using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace OrangeTentacle.SagePay
{
    public class WebSageRequest : SageRequest
    {
        public string Url { get; protected set; }

        public WebSageRequest(string section, string url)
            : base(section)
        {
            Url = url;
        }

        public WebSageRequest(string vendorName, string url, bool filler)
            : base (vendorName, true)
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
            collection.Add("ExpiryDate", Transaction.ExpiryDate.ToShortDateString());
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
                var values = line.Split('=');
                collection.Add(values[0].Trim(), values[1].Trim());
            }

            // response.VPSProtocol = collection["VPSProtocol"];
            response.Status = EnumFromString<TransactionResponse.ResponseStatus>(collection["Status"]);
            response.StatusDetail = collection["StatusDetail"];
            response.VPSTxId = collection["VPSTxId"];
            response.SecurityKey = collection["SecurityKey"];
            response.TxAuthNo = long.Parse(collection["TxAuthNo"]);
            response.AVSCV2 = EnumFromString<TransactionResponse.CV2Status>(collection["AVSCV2"]);
            response.AddressResult = EnumFromString<TransactionResponse.MatchStatus>(collection["AddressResult"]);
            response.PostCodeResult = EnumFromString<TransactionResponse.MatchStatus>(collection["PostCodeResult"]);
            response.CV2Result = EnumFromString<TransactionResponse.MatchStatus>(collection["CV2Result"]);
            response.ThreeDSecure = EnumFromString<TransactionResponse.ThreeDSecureStatus>(collection["3DSecureStatus"]);
            response.Caav = collection["CAVV"];

            return response;

        }

        private static T EnumFromString<T>(string value)
        {
            value = value.Replace(" ", "");
            return (T) Enum.Parse(typeof (T), value, true);
        }

        public override TransactionResponse Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");

            WebClient client = new WebClient();
            //var response = client.UploadValues(Url, Render());

            return new TransactionResponse();
        }
    }
}