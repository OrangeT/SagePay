using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Xunit;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Request.Payment;
using OrangeTentacle.SagePayTest.Configuration;
using OrangeTentacle.SagePayTest.Response;
using Xunit.Extensions;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    
    public class WebSagePayment
    {
        public const string TEST_URL = "http://testserver/";

        // Request fields taken from Protocol spec.
        public static readonly IEnumerable<object[]> REQUEST_FIELDS = 
            new[] {
                new [] {"VPSProtocol"},
                new [] {"TxType"},
                new [] {"Vendor"},
                new [] {"VendorTxCode"},
                new [] {"Amount"},
                new [] {"Currency"},
                new [] {"Description"},
                new [] {"CardHolder"},
                new [] {"CardNumber"},
                new [] {"ExpiryDate"},
                new [] {"CV2"},
                new [] {"CardType"},
                new [] {"BillingSurname"},
                new [] {"BillingFirstnames"},
                new [] {"BillingAddress1"},
                new [] {"BillingCity"},
                new [] {"BillingPostCode"},
                new [] {"BillingCountry"},
                new [] {"DeliverySurname"},
                new [] {"DeliveryFirstnames"},
                new [] {"DeliveryAddress1"},
                new [] {"DeliveryCity"},
                new [] {"DeliveryPostCode"},
                new [] { "DeliveryCountry"}};

        
        public class Constructor
        {

            [Fact]
            public void TakesAnEndPointAndSection()
            {
                var request = new SagePay.Request.Payment.WebSagePayment(SageConfiguration.CONFIG_TYPE, TEST_URL);
                Assert.False(string.IsNullOrWhiteSpace(request.Url));
            }

            [Fact]
            public void TakesAnEndPointAndVendorName()
            {
                var request = new SagePay.Request.Payment.WebSagePayment("bob", TEST_URL, true);
                Assert.False(string.IsNullOrWhiteSpace(request.Url));
            }
        }

        
        public class Encode
        {
            [Theory, MemberData("REQUEST_FIELDS", MemberType = typeof(WebSagePayment))]
            public void AllValuesInCollection(string key)
            {
                var request = new SagePay.Request.Payment.WebSagePayment(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = PaymentRequest.SampleRequest();
                var encode = request.Encode();

                Assert.NotNull(encode[key]);
                Assert.False(string.IsNullOrWhiteSpace(encode[key]));
            }

            [Fact]
            public void ExpiryDateFormat()
            {
                var request = new SagePay.Request.Payment.WebSagePayment(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = PaymentRequest.SampleRequest();
                var encode = request.Encode();
                
                Assert.Equal(string.Format("{0:MMyy}", request.Transaction.ExpiryDate),encode["ExpiryDate"]);
            }
            
            [Theory]
            [InlineData(SagePay.Request.Payment.PaymentRequest.PaymentType.AuthenticateOnly, "AUTHENTICATE ONLY")]
            [InlineData(SagePay.Request.Payment.PaymentRequest.PaymentType.Payment, "PAYMENT")]
            [InlineData(SagePay.Request.Payment.PaymentRequest.PaymentType.Deferred, "DEFERRED")]
            public void TxTypeTypes(SagePay.Request.Payment.PaymentRequest.PaymentType type, string expected)
            {
                var request = new SagePay.Request.Payment.WebSagePayment(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = PaymentRequest.SampleRequest();
                request.Transaction.TxType = type;
                var encode = request.Encode();

                Assert.Equal(encode["TxType"], expected);
            }

            [Theory]
            [InlineData(Currency.GBP)]
            [InlineData(Currency.USD)]
            public void CurrencyTypes(Currency currency)
            {
                var request = new SagePay.Request.Payment.WebSagePayment(SageConfiguration.CONFIG_TYPE, TEST_URL);
                request.Transaction = PaymentRequest.SampleRequest();
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
                var response = new FakePaymentTextResponse();
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(response.Collection["VPSProtocol"], decode.VPSProtocol);
                Assert.Equal(response.Collection["Status"], decode.Status.ToString().ToUpper());
                Assert.Equal(response.Collection["StatusDetail"], decode.StatusDetail);
                Assert.Equal(response.Collection["VPSTxId"], decode.VPSTxId);
                Assert.Equal(response.Collection["SecurityKey"], decode.SecurityKey);
                Assert.Equal(response.Collection["TxAuthNo"], decode.TxAuthNo.ToString());
                Assert.Equal(response.Collection["AVSCV2"].Replace(" ", ""), decode.AVSCV2.ToString().ToUpper());
                Assert.Equal(response.Collection["AddressResult"], decode.AddressResult.ToString().ToUpper());
                Assert.Equal(response.Collection["PostCodeResult"], decode.PostCodeResult.ToString().ToUpper());
                Assert.Equal(response.Collection["CV2Result"], decode.CV2Result.ToString().ToUpper());
                Assert.Equal(response.Collection["3DSecureStatus"], decode.ThreeDSecure.ToString().ToUpper());
                Assert.Equal(response.Collection["CAVV"], decode.Caav);
            }
            
            [Theory]
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
                var response = new FakePaymentTextResponse();
                response.Collection["Status"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.Status);
            }

            [Theory]
            [InlineData(SagePay.Response.CV2Status.AllMatch, "ALL MATCH")]
            [InlineData(SagePay.Response.CV2Status.SecurityCodeMatchOnly, "SECURITY CODE MATCH ONLY")]
            [InlineData(SagePay.Response.CV2Status.AddressMatchOnly, "ADDRESS MATCH ONLY")]
            [InlineData(SagePay.Response.CV2Status.NoDataMatches, "NO DATA MATCHES")]
            [InlineData(SagePay.Response.CV2Status.DataNotChecked, "DATA NOT CHECKED")]
            public void AVSCV2Types(SagePay.Response.CV2Status status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["AVSCV2"] = code;
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.AVSCV2);
            }

            [Theory]
            [InlineData(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [InlineData(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [InlineData(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [InlineData(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void AddressResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["AddressResult"] = code;
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.AddressResult);
            }

            [Theory]
            [InlineData(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [InlineData(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [InlineData(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [InlineData(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void PostCodeResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["PostCodeResult"] = code;
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.PostCodeResult);
            }

            [Theory]
            [InlineData(SagePay.Response.MatchStatus.NotProvided, "NOT PROVIDED")]
            [InlineData(SagePay.Response.MatchStatus.NotChecked, "NOT CHECKED")]
            [InlineData(SagePay.Response.MatchStatus.Matched, "MATCHED")]
            [InlineData(SagePay.Response.MatchStatus.NotMatched, "NOT MATCHED")]
            public void CV2ResultTypes(SagePay.Response.MatchStatus status, string code)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["CV2Result"] = code;
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.CV2Result);
            }

            [Theory]
            [InlineData(SagePay.Response.ThreeDSecureStatus.Error)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.AttemptOnly)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.CantAuth)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.Incomplete)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.Invalid)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.Malformed)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.NoAuth)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.NotAuthed)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.NotChecked)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.OK)]
            [InlineData(SagePay.Response.ThreeDSecureStatus.OK)]
            public void ThreeDSecureTypes(SagePay.Response.ThreeDSecureStatus status)
            {
                var response = new FakePaymentTextResponse();
                response.Collection["3DSecureStatus"] = status.ToString().ToUpper();
                var decode = SagePay.Request.Payment.WebSagePayment.Decode(response.Write());

                Assert.Equal(status, decode.ThreeDSecure);
            }


        }
    }
}