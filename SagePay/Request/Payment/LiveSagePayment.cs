namespace OrangeTentacle.SagePay.Request.Payment
{
    public class LiveSagePayment : WebSagePayment
    {
        private const string _url = "https://live.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Live;

        public LiveSagePayment()
            : base (_type, _url)
        {
        }

        public LiveSagePayment(string vendorName)
            : base(vendorName, _url, true)
        {
        }

    }
}