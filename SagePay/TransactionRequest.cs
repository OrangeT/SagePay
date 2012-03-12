using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OrangeTentacle.SagePay
{
    public class TransactionRequest
    {
        public string VPSProtocol { get { return "2.23"; } }
        public PaymentType TxType { get; set; }
        public string VendorTxCode { get; set; }

        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }

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

            if (String.IsNullOrWhiteSpace(VendorTxCode))
                errors.Add(new ValidationError { Field = "VendorTxCode", Message = "Must Not Be Blank" });

            if (Amount <= 0)
                errors.Add(new ValidationError { Field = "Amount", Message = "Must Not Greater Than Zero" });

            if (String.IsNullOrWhiteSpace(Description))
                errors.Add(new ValidationError { Field = "Description", Message = "Must Not Be Blank" });

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

        public enum PaymentType
        {
            Payment,
            Deferred,
            AuthenticateOnly
        }
    }

    public enum Currency
    {
        GBP,
        USD
    }
}