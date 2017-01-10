using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTentacle.SagePay.Forms
{
    public class LiveSageForm : SageForm
    {
        public LiveSageForm(ProviderTypes type) : base(type)
        {
            SetUrl();
        }

        public LiveSageForm(string vendorName, string encodeKey) : base(vendorName, encodeKey)
        {
            SetUrl();
        }

        private void SetUrl()
        {
            Url = "https://live.sagepay.com/gateway/service/vspform-register.vsp";
        }
    }
}
