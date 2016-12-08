using System.Linq;
using Xunit;

namespace OrangeTentacle.SagePayTest.Request
{
    
    public class TransactionAddress
    {
        
        public class Validate
        {
            [Fact]
            public void IsValid()
            {
                var address = SampleAddress();
                var errors = address.Validate();

                Assert.Empty(errors);
            }

            [Fact]
            public void SurnameMissing()
            {
                var address = SampleAddress();
                address.Surname = string.Empty;

                var errors = address.Validate();
                Assert.Equal("Surname", errors.First().Field);
            }

            [Fact]
            public void FirstnamesMissing()
            {
                var address = SampleAddress();
                address.Firstnames = string.Empty;

                var errors = address.Validate();
                Assert.Equal("Firstnames", errors.First().Field);                
            }

            [Fact]
            public void Address1Missing()
            {
                var address = SampleAddress();
                address.Address1 = string.Empty;

                var errors = address.Validate();
                Assert.Equal("Address1", errors.First().Field);                                
            }

            [Fact]
            public void CityMissing()
            {
                var address = SampleAddress();
                address.City = string.Empty;

                var errors = address.Validate();
                Assert.Equal("City", errors.First().Field);                                
            }

            [Fact]
            public void PostCodeMissing()
            {
                var address = SampleAddress();
                address.PostCode = string.Empty;

                var errors = address.Validate();
                Assert.Equal("Postcode", errors.First().Field);                
            }

            [Fact]
            public void CountryMissing()
            {
                var address = SampleAddress();
                address.Country = string.Empty;

                var errors = address.Validate();
                Assert.Equal("Country", errors.First().Field);                
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