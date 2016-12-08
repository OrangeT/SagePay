using System;
using System.Linq;
using System.Reflection;
using Xunit;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Request.Payment;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    
    public class PaymentRequest
    {
        
        public class Validate
        {
            [Fact]
            public void AllRequiredFields()
            {
                var request = SampleRequest();
                var errors = request.Validate();

                Assert.Equal(0, errors.Count);
            }

            [Fact]
            public void VendorTxCodeMissing()
            {
                var request = SampleRequest();
                request.VendorTxCode = string.Empty;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("VendorTxCode", errors.First().Field);
            }

            [Fact]
            public void AmountMissing()
            {
                var request = SampleRequest();
                request.Amount = 0;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("Amount", errors.First().Field);
            }

            [Fact]
            public void DescriptionMissing()
            {
                var request = SampleRequest();
                request.Description = string.Empty;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("Description", errors.First().Field);
            }

            [Fact]
            public void CardHolderNameMissing()
            {
                var request = SampleRequest();
                request.CardHolderName = "";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CardHolderName", errors.First().Field);
            }

            [Fact]
            public void CardNumberMissing()
            {
                var request = SampleRequest();
                request.CardNumber = "";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Fact]
            public void CardNumberNotNumeric()
            {
                var request = SampleRequest();
                request.CardNumber = "49290000dd006";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Fact]
            public void CardNumberHasSpaces()
            {
                var request = SampleRequest();
                request.CardNumber = "4929 0000 000 06";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Fact]
            public void CardNumberFailsChecksumMissingDigit()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA.Substring(1);
                var errors = request.Validate();

                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Fact]
            public void CardNumberFailsChecksumExtraDigit()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA + "1";
                var errors = request.Validate();

                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Fact]
            public void CardNumberFailsChecksumTranscription()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA.Substring(0, 3) + "4" + SampleCard.VISA.Substring(5);
                var errors = request.Validate();

                Assert.Equal("CardNumber", errors.First().Field);
            }

            [Theory, MemberData("AllCards", MemberType = typeof(SampleCard))]
            public void CardType_Test(SampleCard card)
            {
                var request = SampleRequest();
                request.CardType = card.CardType;
                request.CardNumber = card.Number;
                var errors = request.Validate();

                Assert.Equal(0, errors.Count);
            }

            [Fact]
            public void ExpiryDateInPast()
            {
                var request = SampleRequest();
                request.ExpiryDate = DateTime.Now.AddDays(-1);
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("ExpiryDate", errors.First().Field);
            }

            [Fact]
            public void CV2TooShort()
            {
                var request = SampleRequest();
                request.CV2 = "12";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CV2", errors.First().Field);
            }

            [Fact]
            public void CV2TooLong()
            {
                var request = SampleRequest();
                request.CV2 = "1234";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CV2", errors.First().Field);
            }


            [Fact]
            public void CV2NotNumeric()
            {
                var request = SampleRequest();
                request.CV2 = "12w";
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("CV2", errors.First().Field);
            }

            [Fact]
            public void DeliveryAddressMissing()
            {
                var request = SampleRequest();
                request.Delivery = null;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("Delivery", errors.First().Field);
            }

            [Fact]
            public void BillingAddressMissing()
            {
                var request = SampleRequest();
                request.Billing = null;
                var errors = request.Validate();

                Assert.Equal(1, errors.Count);
                Assert.Equal("Billing", errors.First().Field);
            }

            [Fact]
            public void AddressBubblesErrors()
            {
                var request = SampleRequest();
                request.Delivery.Address1 = string.Empty;
                var errors = request.Validate();

                Assert.Equal(2, errors.Count);
                Assert.Equal("Delivery.Address1", errors[0].Field);
                Assert.Equal("Billing.Address1", errors[1].Field);
            }
        }

        public static SagePay.Request.Payment.PaymentRequest SampleRequest()
        {
            var request = new SagePay.Request.Payment.PaymentRequest();

            request.VendorTxCode = Guid.NewGuid().ToString();
            request.Amount = 100;
            request.Description = "Lorem Ipsum";

            request.CardHolderName = "MR K R RYAN";
            request.CardType = CardType.Visa;
            request.CardNumber = SampleCard.VISA;
            request.ExpiryDate = DateTime.Now.AddMonths(6);
            request.CV2 = SampleCard.CV2;

            request.Billing = request.Delivery = TransactionAddress.SampleAddress();

            return request;
        }
    }
}