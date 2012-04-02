namespace OrangeTentacle.SagePay.Request.Refund
{
    public class TestSageRefund : WebSageRefund
    {
        private const string _url = "https://test.sagepay.com/gateway/service/refund.vsp";
        private const ProviderTypes _type = ProviderTypes.Test;

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