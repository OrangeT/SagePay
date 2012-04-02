using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePayTest.Configuration;
using OrangeTentacle.SagePayTest.Response;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    [TestFixture]
    public class WebSageRequest
    {
        public const string TEST_URL = "http://testserver/";

        // Request fields taken from Protocol spec.
        public static readonly string[] REQUEST_FIELDS = new[] {"VPSProtocol", "TxType", "Vendor", "VendorTxCode", "Amount", "Currency",
                    "Description", "CardHolder", "CardNumber", "ExpiryDate", "CV2", "CardType", "BillingSurname",
                    "BillingFirstnames", "BillingAddress1", "BillingCity", "BillingPostCode", "BillingCountry",
                    "DeliverySurname", "DeliveryFirstnames", "DeliveryAddress1", "DeliveryCity", "DeliveryPostCode",
                    "DeliveryCountry"};

        [TestFixture]
        internal class Constructor
        {

            [Test]
            public void TakesAnEndPointAndSection()
            {
                var request = new SagePay.Request.Payment.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                Assert.IsFalse(string.IsNullOrWhiteSpace(request.Url));
            }

            [Test]
            public void TakesAnEndPointAndVendorName()
            {
                var request = new SagePay.Request.Payment.WebSageRequest("bob", TEST_URL, true);
                Assert.IsFalse(string.IsNullOrWhiteSpace(request.Url));
            }
        }

        [TestFixture]
        internal class Encode
        {
            [Test]
            [Factory(typeof(WebSageRequest), "REQUEST_FIELDS")]
            public void AllValuesInCollection(string key)
            {
                var request = new SagePay.Request.Payment.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                var encode = request.Encode();

                Assert.IsNotNull(encode[key], "Key Not Found");
                Assert.IsFalse(string.IsNullOrWhiteSpace(encode[key]));
            }

            [Test]
            public void ExpiryDateFormat()
            {
                var request = new SagePay.Request.Payment.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                var encode = request.Encode();
                
                Assert.AreEqual(string.Format("{0:MMyy}", request.Transaction.ExpiryDate),encode["ExpiryDate"]);
            }

            [Test]
            [Row(SagePay.Request.Payment.TransactionRequest.PaymentType.AuthenticateOnly, "AUTHENTICATE ONLY")]
            [Row(SagePay.Request.Payment.TransactionRequest.PaymentType.Payment, "PAYMENT")]
            [Row(SagePay.Request.Payment.TransactionRequest.PaymentType.Deferred, "DEFERRED")]
            public void TxTypeTypes(SagePay.Request.Payment.TransactionRequest.PaymentType type, string expected)
            {
                var request = new SagePay.Request.Payment.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                request.Transaction.TxType = type;
                var encode = request.Encode();

                Assert.AreEqual(encode["TxType"], expected);
            }

            [Test]
            [EnumData(typeof(Currency))]
            public void CurrencyTypes(Currency currency)
            {
                var request = new SagePay.Request.Payment.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                request.Transaction.Currency = currency;
                var encode = request.Encode();

                Assert.AreEqual(encode["Currency"], currency.ToString().ToUpper());
            }
        }

        [TestFixture]
        internal class Decode
        {
            [Test]
            public void AllFields()
            {
                var response = new FakePaymentTextResponse();
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(response.Collection["VPSProtocol"], decode.VPSProtocol);
                Assert.AreEqual(response.Collection["Status"], decode.Status.ToString().ToUpper());
                Assert.AreEqual(response.Collection["StatusDetail"], decode.StatusDetail);
                Assert.AreEqual(response.Collection["VPSTxId"], decode.VPSTxId);
                Assert.AreEqual(response.Collection["SecurityKey"], decode.SecurityKey);
                Assert.AreEqual(response.Collection["TxAuthNo"], decode.TxAuthNo.ToString());
                Assert.AreEqual(response.Collection["AVSCV2"].Replace(" ", ""), decode.AVSCV2.ToString().ToUpper());
                Assert.AreEqual(response.Collection["AddressResult"], decode.AddressResult.ToString().ToUpper());
                Assert.AreEqual(response.Collection["PostCodeResult"], decode.PostCodeResult.ToString().ToUpper());
                Assert.AreEqual(response.Collection["CV2Result"], decode.CV2Result.ToString().ToUpper());
                Assert.AreEqual(response.Collection["3DSecureStatus"], decode.ThreeDSecure.ToString().ToUpper());
                Assert.AreEqual(response.Collection["CAVV"], decode.Caav);
            }

            [Test]
            [EnumData(typeof(SagePay.Response.ResponseStatus))]
            public void ResponseStatusTypes(SagePay.Response.ResponseStatus status)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["Status"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.Status);
            }

            [Test]
            [Row(SagePay.Response.CV2Status.AllMatch, "ALL MATCH")]
            [Row(SagePay.Response.CV2Status.SecurityCodeMatchOnly, "SECURITY CODE MATCH ONLY")]
            [Row(SagePay.Response.CV2Status.AddressMatchOnly, "ADDRESS MATCH ONLY")]
            [Row(SagePay.Response.CV2Status.NoDataMatches, "NO DATA MATCHES")]
            [Row(SagePay.Response.CV2Status.DataNotChecked, "DATA NOT CHECKED")]
            public void AVSCV2Types(SagePay.Response.CV2Status status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["AVSCV2"] = code;
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.AVSCV2);
            }

            [Test]
            [Row(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void AddressResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["AddressResult"] = code;
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.AddressResult);
            }

            [Test]
            [Row(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void PostCodeResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["PostCodeResult"] = code;
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.PostCodeResult);
            }

            [Test]
            [Row(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void CV2ResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["CV2Result"] = code;
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.CV2Result);
            }

            [Test]
            [EnumData(typeof(SagePay.Response.ThreeDSecureStatus))]
            public void ThreeDSecureTypes(SagePay.Response.ThreeDSecureStatus status)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["3DSecureStatus"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Payment.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.ThreeDSecure);
            }


        }
    }
}