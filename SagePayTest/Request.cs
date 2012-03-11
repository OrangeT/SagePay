using System;
using System.Linq;
using MbUnit.Framework;
using System.Configuration;
using OrangeTentacle.SagePay;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class OfflineSageRequest
    {
        [TestFixture]
        internal class Constructor
        {
            [Test]
            public void ConfigureFromConfig()
            {
                var request = new SagePay.OfflineSageRequest();
                var section = SagePay.OfflineSageConfiguration.GetSection();


                Assert.AreEqual(section.VendorName, request.Vendor.VendorName);
            }

            [Test]
            public void ConfigureFromConstructor()
            {
                var name = "bob";
                var request = new SagePay.OfflineSageRequest(name);

                Assert.AreEqual(name, request.Vendor.VendorName);
            }
        }

        [TestFixture]
        internal class Validate
        {
            [Test]
            public void ReturnsErrors()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                request.Transaction.CV2 = "12";
                var errors = request.Validate();

                Assert.AreEqual(1, errors.Count);
                Assert.AreEqual("CV2", errors.First().Field);
                Assert.IsFalse(request.IsValid);
            }

            [Test]
            public void SetsIsValid()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                var errors = request.Validate();

                Assert.AreEqual(0, errors.Count);
                Assert.IsTrue(request.IsValid);                
            }
        }

        [TestFixture]
        internal class Send
        {
            [Test]
            [AssertException(typeof (SageException))]
            public void ThrowsAnExceptionIfInvalid()
            {
                var request = new SagePay.OfflineSageRequest();
                var response = request.Send();
            }

            [Test]
            public void EmitsAResponseIsValid()
            {
                var request = new SagePay.OfflineSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();
                request.Validate();

                var response = request.Send();
                Assert.IsNotNull(response);
            }
        }

        [TestFixture]
        internal class VendorRequest
        {
            [TestFixture]
            internal class Constructor
            {

                [Test]
                public void Valid()
                {
                    var name = "bob";
                    var vendor = new OrangeTentacle.SagePay.OfflineSageRequest.VendorRequest(name);
                    Assert.AreEqual(vendor.VendorName, name);
                }

                [Test]
                [AssertException(typeof (ConfigurationErrorsException))]
                public void EmptyIsNotValid()
                {
                    var name = string.Empty;
                    var vendor = new OrangeTentacle.SagePay.OfflineSageRequest.VendorRequest(name);
                }
            }
        }

        [TestFixture]
        internal class TransactionRequest
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
            }

            public static SagePay.OfflineSageRequest.TransactionRequest SampleRequest()
            {
                var request = new SagePay.OfflineSageRequest.TransactionRequest();
                request.CardHolderName = "MR K R RYAN";
                request.CardType = CardType.Visa;
                request.CardNumber = SampleCard.VISA;
                request.ExpiryDate = DateTime.Now.AddMonths(6);
                request.CV2 = SampleCard.CV2;
                return request;
            }
        }

}
}
