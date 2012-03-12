namespace OrangeTentacle.SagePay
{
    public class LiveSageRequest : WebSageRequest
    {
        private const string _url = "https://live.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const string _section = "LiveSagePay";

        public LiveSageRequest()
            : base (_section, _url)
        {
        }

        public LiveSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
        }

    }
}