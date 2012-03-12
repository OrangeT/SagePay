using System;
using System.Linq;
using MbUnit.Framework;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class TransactionRequest
    {

        [TestFixture]
        internal class Validate
        {
            [Test]
            public void AllRequiredFields()
            {
                var request = SampleRequest();
                var errors = request.Validate();

                Assert.AreEqual(0, errors.Count);
            }

            [Test]
            public void CardHolderNameMissing()
            {
                var request = SampleRequest();
                request.CardHolderName = "";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CardHolderName", errors.First().Field);
            }

            [Test]
            public void CardNumberMissing()
            {
                var request = SampleRequest();
                request.CardNumber = "";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            public void CardNumberNotNumeric()
            {
                var request = SampleRequest();
                request.CardNumber = "49290000dd006";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            public void CardNumberHasSpaces()
            {
                var request = SampleRequest();
                request.CardNumber = "4929 0000 000 06";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            public void CardNumberFailsChecksumMissingDigit()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA.Substring(1);
                var errors = request.Validate();

                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            public void CardNumberFailsChecksumExtraDigit()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA + "1";
                var errors = request.Validate();

                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            public void CardNumberFailsChecksumTranscription()
            {
                var request = SampleRequest();
                request.CardNumber = SampleCard.VISA.Substring(0, 3) + "4" + SampleCard.VISA.Substring(5);
                var errors = request.Validate();

                Assert.AreEqual("CardNumber", errors.First().Field);
            }

            [Test]
            [Factory(typeof(SampleCard), "AllCards")]
            public void CardType_Test(SampleCard card)
            {
                var request = SampleRequest();
                request.CardType = card.CardType;
                request.CardNumber = card.Number;
                var errors = request.Validate();

                Assert.AreEqual(0, errors.Count);
            }

            [Test]
            public void ExpiryDateInPast()
            {
                var request = SampleRequest();
                request.ExpiryDate = DateTime.Now.AddDays(-1);
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("ExpiryDate", errors.First().Field);
            }

            [Test]
            public void CV2TooShort()
            {
                var request = SampleRequest();
                request.CV2 = "12";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CV2", errors.First().Field);
            }

            [Test]
            public void CV2TooLong()
            {
                var request = SampleRequest();
                request.CV2 = "1234";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CV2", errors.First().Field);
            }


            [Test]
            public void CV2NotNumeric()
            {
                var request = SampleRequest();
                request.CV2 = "12w";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CV2", errors.First().Field);
            }

            [Test]
            public void DeliveryAddressMissing()
            {
                var request = SampleRequest();
                request.Delivery = null;
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("Delivery", errors.First().Field);
            }

            [Test]
            public void BillingAddressMissing()
            {
                var request = SampleRequest();
                request.Billing = null;
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("Billing", errors.First().Field);
            }

            [Test]
            public void AddressBubblesErrors()
            {
                var request = SampleRequest();
                request.Delivery.Address1 = string.Empty;
                var errors = request.Validate();

                Assert.AreEqual(2, errors.Count);
                Assert.AreEqual("Delivery.Address1", errors[0].Field);
                Assert.AreEqual("Billing.Address1", errors[1].Field);
            }
        }

        public static SagePay.TransactionRequest SampleRequest()
        {
            var request = new SagePay.TransactionRequest();
            request.CardHolderName = "MR K R RYAN";
            request.CardType = CardType.Visa;
            request.CardNumber = SampleCard.VISA;
            request.ExpiryDate = DateTime.Now.AddMonths(6);
            request.CV2 = SampleCard.CV2;

            var address = new SagePay.TransactionAddress();
            request.Billing = request.Delivery = TransactionAddress.SampleAddress();

            return request;
        }
    }

    [TestFixture]
    public class TransactionAddress
    {
        [TestFixture]
        internal class Validate
        {
            [Test]
            public void IsValid()
            {
                var address = SampleAddress();
                var errors = address.Validate();

                Assert.IsEmpty(errors);
            }

            [Test]
            public void SurnameMissing()
            {
                var address = SampleAddress();
                address.Surname = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("Surname", errors.First().Field);
            }

            [Test]
            public void FirstnamesMissing()
            {
                var address = SampleAddress();
                address.Firstnames = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("Firstnames", errors.First().Field);                
            }

            [Test]
            public void Address1Missing()
            {
                var address = SampleAddress();
                address.Address1 = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("Address1", errors.First().Field);                                
            }

            [Test]
            public void CityMissing()
            {
                var address = SampleAddress();
                address.City = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("City", errors.First().Field);                                
            }

            [Test]
            public void PostCodeMissing()
            {
                var address = SampleAddress();
                address.PostCode = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("Postcode", errors.First().Field);                
            }

            [Test]
            public void CountryMissing()
            {
                var address = SampleAddress();
                address.Country = string.Empty;

                var errors = address.Validate();
                Assert.AreEqual("Country", errors.First().Field);                
            }
        }

        public static SagePay.TransactionAddress SampleAddress()
        {
            var address = new SagePay.TransactionAddress
                              {
                                  Surname = "Ryan",
                                  Firstnames = "Kian Ronan",
                                  Address1 = SampleCard.BILLING_ADDRESS,
                                  City = "London",
                                  PostCode = SampleCard.POSTCODE,
                                  Country = "UK"
                              };
            return address;
        }
    }
}