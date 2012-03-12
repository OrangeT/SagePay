using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OrangeTentacle.SagePay
{
    public class TransactionRequest
    {
        public string CardHolderName { get; set; }
        public CardType CardType { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CV2 { get; set; }

        public TransactionAddress Billing { get; set; }
        public TransactionAddress Delivery { get; set; }

        public List<ValidationError> Validate()
        {
            var errors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(CardHolderName))
                errors.Add(new ValidationError { Field = "CardHolderName", Message = "Must Not Be Blank"});

            if (! Regex.IsMatch(CV2, @"^\d{3}$"))
                errors.Add(new ValidationError { Field = "CV2", Message = "Must Be Three Numbers" });

            if (!Regex.IsMatch(CardNumber, @"^\d+$"))
                errors.Add(new ValidationError { Field = "CardNumber", Message = "Must Be Numeric With No Spaces" });
            else if (!Luhn.IsValid(CardNumber))
                errors.Add(new ValidationError { Field = "CardNumber", Message = "Card Number Is Invalid" });

            if (ExpiryDate < DateTime.Now)
                errors.Add(new ValidationError { Field = "ExpiryDate", Message = "Expiry Date Must Not Be In The Past" });

            if (Delivery == null)
                errors.Add(new ValidationError { Field = "Delivery", Message = "Delivery Information Must Be Populated"});
            else
                errors.AddRange(Delivery.Validate("Delivery."));

            if (Billing == null)
                errors.Add(new ValidationError { Field = "Billing", Message = "Delivery Information Must Be Populated" });
            else
                errors.AddRange(Billing.Validate("Billing."));

            return errors;
        }
    }

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