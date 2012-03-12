using System.Collections.Generic;
using System.Linq;

namespace OrangeTentacle.SagePay
{
    public abstract class SageRequest
    {
        public VendorRequest Vendor { get; private set; }
        public TransactionRequest Transaction { get; set; }
        public bool IsValid { get; protected set; }

        public SageRequest(string configSection)
        {
            var config = SageConfiguration.GetSection(configSection);
            Vendor = new VendorRequest(config.VendorName);
        }

        public SageRequest(string vendorName, bool filler)
        {
            Vendor = new VendorRequest(vendorName);
        }

        public List<ValidationError> Validate()
        {
            IsValid = false; // Guilty until proven innocent.

            if (Transaction == null)
                throw new SageException("No Transaction Set");

            var errors = Transaction.Validate();
            IsValid = !errors.Any();
            return errors;
        }

        public abstract TransactionResponse Send();
    }
}