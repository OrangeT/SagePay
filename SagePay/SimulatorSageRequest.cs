namespace OrangeTentacle.SagePay
{
    public class SimulatorSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/Simulator/VSPDirectGateway.asp";
        private const SagePayFactory.ProviderTypes _type = SagePayFactory.ProviderTypes.Simulator;

        public SimulatorSageRequest()
            : base(_type, _url)
        {
            
        }

        public SimulatorSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }
}