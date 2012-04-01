namespace OrangeTentacle.SagePay.Request.Payment
{
    public class LiveSageRequest : WebSageRequest
    {
        private const string _url = "https://live.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Live;

        public LiveSageRequest()
            : base (_type, _url)
        {
        }

        public LiveSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
        }

    }
}