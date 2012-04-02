namespace OrangeTentacle.SagePay.Request.Refund
{
    public class LiveSageRefund : WebSageRefund
    {
        private const string _url = "https://live.sagepay.com/gateway/service/refund.vsp";
        private const ProviderTypes _type = ProviderTypes.Live;

        public SimulatorSageRefund()
            : base(_type, _url)
        {
            
        }

        public SimulatorSageRefund(string vendorName)
            : base(vendorName, _url)
        {
            
        }

    }
}