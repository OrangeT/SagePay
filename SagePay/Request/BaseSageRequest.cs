using System.Collections.Generic;
using System.Linq;
using OrangeTentacle.SagePay.Configuration;
using OrangeTentacle.SagePay.Request.Payment;

namespace OrangeTentacle.SagePay.Request
{
    public abstract class BaseSageRequest
    {
        public VendorRequest Vendor { get; protected set; }

        protected bool _isValid;

        public bool IsValid
        {
            get { return _isValid; }
            protected set { _isValid = value; }
        }

        public BaseSageRequest(ProviderTypes type)
        {
            var config = SageConfiguration.GetSection(type);
            Vendor = new VendorRequest(config.VendorName);
        }

        public BaseSageRequest(string vendorName)
        {
            Vendor = new VendorRequest(vendorName);
        }

        protected static List<ValidationError> Validate(IValidate toValidate, out bool IsValid)
        {
            IsValid = false; // Guilty until proven innocent.

            if (toValidate == null)
                throw new SageException("No Payment Set");

            var errors = toValidate.Validate();
            IsValid = !errors.Any();
            return errors;
        }
    }
}
