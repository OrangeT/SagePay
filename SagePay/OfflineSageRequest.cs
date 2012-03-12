using System.Collections.Generic;
using System.Linq;

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
    }
}
