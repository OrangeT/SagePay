using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace OrangeTentacle.SagePay
{
    public class OfflineSageRequest
    {
        public VendorRequest Vendor { get; private set; }
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

        public void Validate()
        {
            IsValid = true;
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

    public class Luhn
    {
        public static bool IsValid(string number)
        {
            var reversed = number.ToCharArray();
            Array.Reverse(reversed);

            var sum = 0;
            for (int i = 0; i < number.Length; i++ )
            {
                var result = char.GetNumericValue(reversed[i]);
                if (result == -1)
                    return false;

                if (i % 2 == 0)
                    sum += (int)result;
                else
                {
                    result = result*2;
                    var tens = (int) (result/10);
                    result = tens + result - (tens * 10);
                    sum += (int)result;
                }
            }
            return sum % 10 == 0;
        }
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }

    public enum CardType
    {
        Visa,
        Mc,
        Delta,
        Maestro,
        Uke,
        Amex,
        Dc,
        Jcb,
        Laser
    }

    public class OfflineSageConfiguration : ConfigurationSection
    {
        private const string sectionName = "OfflineSageConfiguration";

        public static OfflineSageConfiguration GetSection()
        {
            return ConfigurationManager.GetSection(sectionName) as OfflineSageConfiguration;
        }

        [ConfigurationProperty("vendorName", IsRequired = true)]
        public string VendorName
        {
            get { return (string)this["vendorName"]; }
        }
    }

    public class SageException : Exception
    {
        public SageException()
        {}

        public SageException(string message) : base(message)
        {}
    }
}
