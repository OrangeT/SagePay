namespace OrangeTentacle.SagePay.Request.Refund
{
    public class SimulatorSageRefund : WebSageRefund
    {
        private const string _url = "https://test.sagepay.com/Simulator/VSPServerGateway.asp?service=Refund";
        private const ProviderTypes _type = ProviderTypes.Simulator;

        public SimulatorSageRefund()
            : base(_type, _url)
        {
            
        }

        public SimulatorSageRefund(string vendorName)
            : base(vendorName, _url)
        {
            
        }

    }

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