namespace OrangeTentacle.SagePay.Request.Payment
{
    public class SimulatorSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/Simulator/VSPDirectGateway.asp";
        private const ProviderTypes _type = ProviderTypes.Simulator;

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