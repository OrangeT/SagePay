using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageRequest
    {
        public VendorRequest Vendor { get; private set; }
        public TransactionRequest Transaction { get; set; }
        public bool IsValid { get; private set; }

        public OfflineSageRequest()
        {
            var config = OfflineSageConfiguration.GetSection();
            Vendor = new VendorRequest(config.VendorName);
        }

        public OfflineSageRequest(string name)
        {
            Vendor = new VendorRequest(name);
        }

        public List<ValidationError> Validate()
        {
            IsValid = false; // Guilty until proven innocent.

            if (Transaction == null)
                throw new SageException("No Transaction Set");

            var errors = Transaction.Validate();
            IsValid = ! errors.Any();
            return errors;
        }

        public object Send()
        {
            if (! IsValid)
                throw new SageException("Configuration Must Be Valid");
            return "";
        }

        public class VendorRequest
        {
            public string VendorName { get; private set; }

            public VendorRequest(string vendorName)
            {
                if (string.IsNullOrWhiteSpace(vendorName))
                    throw new ConfigurationErrorsException("Vendor Must Have VendorName");

                VendorName = vendorName;
            }
        }

        public class TransactionRequest
        {
            public string CardHolderName { get; set; }
            public CardType CardType { get; set; }
            public string CardNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string CV2 { get; set; }

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

                return errors;
            }
        }


    }
}
