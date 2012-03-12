using System.Configuration;

namespace OrangeTentacle.SagePay
{
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
}