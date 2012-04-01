using System.Collections.Generic;

namespace OrangeTentacle.SagePay.Request
{
    public class TransactionAddress
    {
        public string Surname { get; set; }
        public string Firstnames { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public bool IsValid { get; set; }
 
        public List<ValidationError> Validate(string prefix = "")
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(Surname))
                errors.Add(new ValidationError {Field = prefix + "Surname", Message = "Must Not By Empty"});

            if (string.IsNullOrWhiteSpace(Firstnames))
                errors.Add(new ValidationError { Field = prefix + "Firstnames", Message = "Must Not By Empty" });

            if (string.IsNullOrWhiteSpace(Address1))
                errors.Add(new ValidationError { Field = prefix + "Address1", Message = "Must Not By Empty" });

            if (string.IsNullOrWhiteSpace(City))
                errors.Add(new ValidationError { Field = prefix + "City", Message = "Must Not By Empty" });

            if (string.IsNullOrWhiteSpace(PostCode))
                errors.Add(new ValidationError { Field = prefix + "Postcode", Message = "Must Not By Empty" });

            if (string.IsNullOrWhiteSpace(Country))
                errors.Add(new ValidationError { Field = prefix + "Country", Message = "Must Not By Empty" });

            return errors;
        }
    }
}