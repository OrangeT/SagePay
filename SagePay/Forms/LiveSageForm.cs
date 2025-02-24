﻿namespace OrangeTentacle.SagePay.Forms
{
    public class LiveSageForm : SageForm
    {
        private const string _url = "https://live.opayo.eu.elavon.com/gateway/service/vspform-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Live;

        public LiveSageForm()
            : base(_type, _url)
        {
        }

        public LiveSageForm(string vendorName, string encodeKey)
            : base(vendorName, encodeKey, _url)
        {
        }
    }
}

