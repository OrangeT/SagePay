namespace OrangeTentacle.SagePay
{
    public class LiveSageRequest : WebSageRequest
    {
        private const string _url = "https://live.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const SagePayFactory.ProviderTypes _type = SagePayFactory.ProviderTypes.Live;

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