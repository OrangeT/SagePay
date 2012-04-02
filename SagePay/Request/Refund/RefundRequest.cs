using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrangeTentacle.SagePay.Request.Payment;

namespace OrangeTentacle.SagePay.Request.Refund
{
    public class RefundRequest : IValidate
    {
        public string VPSProtocol { get { return "2.23"; } }
        public string VendorTxCode { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public string RelatedVPSTxId { get; set; }
        public string RelatedVendorTxCode { get; set; }
        public string RelatedSecurityKey { get; set; }
        public long RelatedTxAuthNo { get; set; }


        public List<ValidationError> Validate()
        {
            var list = new List<ValidationError>();

            if (String.IsNullOrEmpty(VendorTxCode))
                list.Add(new ValidationError{ Field = "VendorTxCode", Message = "Must Not Be Blank"});

            if (Amount <= 0)
                list.Add(new ValidationError { Field = "Amount", Message = "Must Not Greater Than Zero" });

            if (String.IsNullOrEmpty(Description))
                list.Add(new ValidationError { Field = "Description", Message = "Must Not Be Blank" });

            if (String.IsNullOrEmpty(RelatedVPSTxId))
                list.Add(new ValidationError { Field = "RelatedVPSTxId", Message = "Must Not Be Blank" });

            if (String.IsNullOrEmpty(RelatedVendorTxCode))
                list.Add(new ValidationError { Field = "RelatedVendorTxCode", Message = "Must Not Be Blank" });

            if (String.IsNullOrEmpty(RelatedSecurityKey))
                list.Add(new ValidationError { Field = "RelatedSecurityKey", Message = "Must Not Be Blank" });

            if (RelatedTxAuthNo <= 0)
                list.Add(new ValidationError { Field = "RelatedTxAuthNo", Message = "Must Not Be Blank" });

            return list;
        }
    }
}
