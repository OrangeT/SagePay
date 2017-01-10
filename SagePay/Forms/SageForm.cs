using System.Collections.Generic;
using System.Linq;
using OrangeTentacle.SagePay.Configuration;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Sugar;

namespace OrangeTentacle.SagePay.Forms
{
    public abstract class SageForm : BaseSageRequest
    {
        public string Url { get; protected set; }

        public string EncodeKey { get; protected set; }

        public Dictionary<string, string> BodyParameters { get; protected set; }

        private Dictionary<string, string> _cryptParameters;

        protected SageForm(ProviderTypes type, string url)
            : base(type)
        {
            var config = SageConfiguration.GetSection(type);

            EncodeKey = config.EncodeKey;
            Url = url;

            SetBaseParams();
        }

        protected SageForm(string vendorName, string encodeKey, string url)
            : base(vendorName)
        {
            EncodeKey = encodeKey;
            Url = url;

            SetBaseParams();
        }

        private void SetBaseParams()
        {
            BodyParameters = new Dictionary<string, string>();
            _cryptParameters = new Dictionary<string, string>();

            BodyParameters.Add("VPSProtocol", "3.00");
            BodyParameters.Add("TxType", "PAYMENT");
            BodyParameters.Add("Vendor", Vendor.VendorName);
        }

        public void AddCryptParameter(string key, string value)
        {
            if (_cryptParameters.ContainsKey(key))
            {
                _cryptParameters[key] = value;
            }
            else
            {
                _cryptParameters.Add(key, value);
            }

            BuildCrypt();
        }

        private void BuildCrypt()
        {
            var unencoded = string.Join("&",
                _cryptParameters.Select(x => string.Format("{0}={1}", x.Key, x.Value))
                    .ToArray());

            var crypt = Crypto.Encode(EncodeKey, unencoded);

            if (! BodyParameters.ContainsKey("crypt"))
            {
                BodyParameters.Add("crypt", crypt);
            }
            else
            {
                BodyParameters["crypt"] = crypt;
            }
        }

        public List<ValidationError> Validate(out bool isValid)
        {
            // Validate required parameters

            var errors = new List<ValidationError>();

            ValidateParameter(errors, "BillingSurname");
            ValidateParameter(errors, "BillingFirstnames");
            ValidateParameter(errors, "BillingAddress1");
            ValidateParameter(errors, "BillingCity");
            ValidateParameter(errors, "BillingPostCode");
            ValidateParameter(errors, "BillingCountry");

            ValidateParameter(errors, "DeliverySurname");
            ValidateParameter(errors, "DeliveryFirstnames");
            ValidateParameter(errors, "DeliveryAddress1");
            ValidateParameter(errors, "DeliveryCity");
            ValidateParameter(errors, "DeliveryPostCode");
            ValidateParameter(errors, "DeliveryCountry");

            ValidateParameter(errors, "VendorTxCode");
            ValidateParameter(errors, "Amount");
            ValidateParameter(errors, "Currency");
            ValidateParameter(errors, "Description");
            ValidateParameter(errors, "SuccessURL");
            ValidateParameter(errors, "FailureURL");

            isValid = errors.Any();

            return errors;
        }

        private void ValidateParameter(ICollection<ValidationError> errors, string key)
        {
            if (string.IsNullOrEmpty(BodyParameters[key]))
            {
                errors.Add(new ValidationError { Field = key, Message = "Must be present" });
            }
        }
    }
}
 
