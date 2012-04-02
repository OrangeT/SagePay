using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrangeTentacle.SagePay.Response
{
    public class RefundResponse
    {
        public string VPSProtocol { get { return "2.23"; } }
        public ResponseStatus Status { get; set; }
        public string StatusDetail { get; set; }
        public string VPSTxId { get; set; }
        public long TxAuthNo { get; set; }

        public bool IsValid()
        {
            return Status == ResponseStatus.OK;
        }
    }
}
