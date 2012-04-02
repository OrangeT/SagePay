using MbUnit.Framework;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePayTest.Configuration;
using OrangeTentacle.SagePayTest.Response;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    [TestFixture]
    public class WebSageRefund
    {
        public const string TEST_URL = "http://testserver/";

        // Request fields taken from Protocol spec.
        public static readonly string[] REQUEST_FIELDS = new[] {"VPSProtocol", "TxType", "Vendor", "VendorTxCode", "Amount", "Currency", "Description", "RelatedVPSTxId",
            "RelatedVendorTxCode", "RelatedSecurityKey", "RelatedTxAuthNo"};

        [TestFixture]
        internal class Constructor
        {

            [Test]
            public void TakesAnEndPointAndSection()
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                Assert.IsFalse(string.IsNullOrWhiteSpace(request.Url));
            }

            [Test]
            public void TakesAnEndPointAndVendorName()
            {
                var request = new SagePay.Request.Refund.WebSageRefund("bob", TEST_URL);
                Assert.IsFalse(string.IsNullOrWhiteSpace(request.Url));
            }
        }

        [TestFixture]
        internal class Encode
        {
            [Test]
            [Factory(typeof(WebSageRefund), "REQUEST_FIELDS")]
            public void AllValuesInCollection(string key)
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = RefundRequest.SampleRequest();
                var encode = request.Encode();

                Assert.IsNotNull(encode[key], "Key Not Found");
                Assert.IsFalse(string.IsNullOrWhiteSpace(encode[key]));
            }

            [Test]
            [EnumData(typeof(Currency))]
            public void CurrencyTypes(Currency currency)
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = RefundRequest.SampleRequest();
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
                var response = new FakeRefundTextResponse();
                var decode = SagePay.Request.Refund.WebSageRefund.Decode(response.Write());

                Assert.AreEqual(response.Collection["VPSProtocol"], decode.VPSProtocol);
                Assert.AreEqual(response.Collection["Status"], decode.Status.ToString().ToUpper());
                Assert.AreEqual(response.Collection["StatusDetail"], decode.StatusDetail);
                Assert.AreEqual(response.Collection["VPSTxId"], decode.VPSTxId);
                Assert.AreEqual(response.Collection["TxAuthNo"], decode.TxAuthNo.ToString());
            }

            [Test]
            [EnumData(typeof(SagePay.Response.ResponseStatus))]
            public void ResponseStatusTypes(SagePay.Response.ResponseStatus status)
            {
                var response = new FakeRefundTextResponse();
                response.Collection["Status"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Refund.WebSageRefund.Decode(response.Write());

                Assert.AreEqual(status, decode.Status);
            }

        }

    }
}