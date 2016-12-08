using System.Collections.Generic;
using Xunit;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePayTest.Configuration;
using OrangeTentacle.SagePayTest.Response;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    
    public class WebSageRefund
    {
        public const string TEST_URL = "http://testserver/";

        // Request fields taken from Protocol spec.
        public static readonly IEnumerable<object[]> REQUEST_FIELDS = new[] {
            new [] {"VPSProtocol"},
            new [] {"TxType"},
            new [] {"Vendor"},
            new [] {"VendorTxCode"},
            new [] {"Amount"},
            new [] {"Currency"},
            new [] {"Description"},
            new [] {"RelatedVPSTxId"},
            new [] {"RelatedVendorTxCode"},
            new [] {"RelatedSecurityKey"},
            new [] { "RelatedTxAuthNo"} };

        
        public class Constructor
        {

            [Fact]
            public void TakesAnEndPointAndSection()
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                Assert.False(string.IsNullOrWhiteSpace(request.Url));
            }

            [Fact]
            public void TakesAnEndPointAndVendorName()
            {
                var request = new SagePay.Request.Refund.WebSageRefund("bob", TEST_URL);
                Assert.False(string.IsNullOrWhiteSpace(request.Url));
            }
        }

        
        public class Encode
        {
            [Theory, MemberData("REQUEST_FIELDS", MemberType = typeof(WebSageRefund))]
            public void AllValuesInCollection(string key)
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = RefundRequest.SampleRequest();
                var encode = request.Encode();

                Assert.NotNull(encode[key]);
                Assert.False(string.IsNullOrWhiteSpace(encode[key]));
            }

            [Theory]
            [InlineData(Currency.USD)]
            [InlineData(Currency.GBP)]
            public void CurrencyTypes(Currency currency)
            {
                var request = new SagePay.Request.Refund.WebSageRefund(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = RefundRequest.SampleRequest();
                request.Transaction.Currency = currency;
                var encode = request.Encode();

                Assert.Equal(encode["Currency"], currency.ToString().ToUpper());
            }
        }

        
        public class Decode
        {
            [Fact]
            public void AllFields()
            {
                var response = new FakeRefundTextResponse();
                var decode = SagePay.Request.Refund.WebSageRefund.Decode(response.Write());

                Assert.Equal(response.Collection["VPSProtocol"], decode.VPSProtocol);
                Assert.Equal(response.Collection["Status"], decode.Status.ToString().ToUpper());
                Assert.Equal(response.Collection["StatusDetail"], decode.StatusDetail);
                Assert.Equal(response.Collection["VPSTxId"], decode.VPSTxId);
                Assert.Equal(response.Collection["TxAuthNo"], decode.TxAuthNo.ToString());
            }

            [Theory]
            [InlineData(SagePay.Response.ResponseStatus.OK)]
            [InlineData(SagePay.Response.ResponseStatus.Authenticated)]
            [InlineData(SagePay.Response.ResponseStatus.Error)]
            [InlineData(SagePay.Response.ResponseStatus.Invalid)]
            [InlineData(SagePay.Response.ResponseStatus.Malformed)]
            [InlineData(SagePay.Response.ResponseStatus.NotAuthed)]
            [InlineData(SagePay.Response.ResponseStatus.OK)]
            [InlineData(SagePay.Response.ResponseStatus.Ppredirect)]
            [InlineData(SagePay.Response.ResponseStatus.Registered)]
            [InlineData(SagePay.Response.ResponseStatus.Rejected)]
            [InlineData(SagePay.Response.ResponseStatus.ThreeDAuth)]
            public void ResponseStatusTypes(SagePay.Response.ResponseStatus status)
            {
                var response = new FakeRefundTextResponse();
                response.Collection["Status"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Refund.WebSageRefund.Decode(response.Write());

                Assert.Equal(status, decode.Status);
            }

        }

    }
}