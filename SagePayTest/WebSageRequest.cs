using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class WebSageRequest
    {
        public const string TEST_URL = "http://testserver/";

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
                var request = new SagePay.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                Assert.IsFalse(string.IsNullOrWhiteSpace(request.Url));
            }

            [Test]
            public void TakesAnEndPointAndVendorName()
            {
                var request = new SagePay.WebSageRequest("bob", TEST_URL, true);
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
                var request = new SagePay.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                var encode = request.Encode();

                Assert.IsNotNull(encode[key], "Key Not Found");
                Assert.IsFalse(string.IsNullOrWhiteSpace(encode[key]));
            }

            [Test]
            public void ExpiryDateFormat()
            {
                var request = new SagePay.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                var encode = request.Encode();
                
                Assert.AreEqual(string.Format("{0:MMyy}", request.Transaction.ExpiryDate),encode["ExpiryDate"]);
            }

            [Test]
            [Row(SagePay.TransactionRequest.PaymentType.AuthenticateOnly, "AUTHENTICATE ONLY")]
            [Row(SagePay.TransactionRequest.PaymentType.Payment, "PAYMENT")]
            [Row(SagePay.TransactionRequest.PaymentType.Deferred, "DEFERRED")]
            public void TxTypeTypes(SagePay.TransactionRequest.PaymentType type, string expected)
            {
                var request = new SagePay.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = TransactionRequest.SampleRequest();
                request.Transaction.TxType = type;
                var encode = request.Encode();

                Assert.AreEqual(encode["TxType"], expected);
            }

            [Test]
            [EnumData(typeof(Currency))]
            public void CurrencyTypes(Currency currency)
            {
                var request = new SagePay.WebSageRequest(SageConfiguration.CONFIG_TYPE, TEST_URL);
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
                var response = new FakeTextResponse();
                var decode = SagePay.WebSageRequest.Decode(response.Write());

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
            [EnumData(typeof(SagePay.TransactionResponse.ResponseStatus))]
            public void ResponseStatusTypes(SagePay.TransactionResponse.ResponseStatus status)
            {
                var response = new FakeTextResponse();
                response.Collection["Status"] = status.ToString().ToUpper();
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.Status);
            }

            [Test]
            [Row(SagePay.TransactionResponse.CV2Status.AllMatch, "ALL MATCH")]
            [Row(SagePay.TransactionResponse.CV2Status.SecurityCodeMatchOnly, "SECURITY CODE MATCH ONLY")]
            [Row(SagePay.TransactionResponse.CV2Status.AddressMatchOnly, "ADDRESS MATCH ONLY")]
            [Row(SagePay.TransactionResponse.CV2Status.NoDataMatches, "NO DATA MATCHES")]
            [Row(SagePay.TransactionResponse.CV2Status.DataNotChecked, "DATA NOT CHECKED")]
            public void AVSCV2Types(SagePay.TransactionResponse.CV2Status status, string code)
            {
                var response = new FakeTextResponse();
                response.Collection["AVSCV2"] = code;
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.AVSCV2);
            }

            [Test]
            [Row(SagePay.TransactionResponse.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.TransactionResponse.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotMatched, "NOT MATCHED")]
            public void AddressResultTypes(SagePay.TransactionResponse.MatchStatus status, string code)
            {
                var response = new FakeTextResponse();
                response.Collection["AddressResult"] = code;
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.AddressResult);
            }

            [Test]
            [Row(SagePay.TransactionResponse.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.TransactionResponse.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotMatched, "NOT MATCHED")]
            public void PostCodeResultTypes(SagePay.TransactionResponse.MatchStatus status, string code)
            {
                var response = new FakeTextResponse();
                response.Collection["PostCodeResult"] = code;
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.PostCodeResult);
            }

            [Test]
            [Row(SagePay.TransactionResponse.MatchStatus.NotProvided, "NOT PROVIDED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotChecked, "NOT CHECKED")]
            [Row(SagePay.TransactionResponse.MatchStatus.Matched, "MATCHED")]
            [Row(SagePay.TransactionResponse.MatchStatus.NotMatched, "NOT MATCHED")]
            public void CV2ResultTypes(SagePay.TransactionResponse.MatchStatus status, string code)
            {
                var response = new FakeTextResponse();
                response.Collection["CV2Result"] = code;
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.CV2Result);
            }

            [Test]
            [EnumData(typeof(SagePay.TransactionResponse.ThreeDSecureStatus))]
            public void ThreeDSecureTypes(SagePay.TransactionResponse.ThreeDSecureStatus status)
            {
                var response = new FakeTextResponse();
                response.Collection["3DSecureStatus"] = status.ToString().ToUpper();
                var decode = SagePay.WebSageRequest.Decode(response.Write());

                Assert.AreEqual(status, decode.ThreeDSecure);
            }


        }
    }
}