using System.Linq;
using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
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

        public static SagePay.Request.TransactionAddress SampleAddress()
        {
            var address = new SagePay.Request.TransactionAddress 
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