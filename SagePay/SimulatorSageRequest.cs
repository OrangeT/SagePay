namespace OrangeTentacle.SagePay
{
    public class SimulatorSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/Simulator/VSPDirectGateway.asp";
        private const string _section = "SimulatorSagePay";

        public SimulatorSageRequest()
            : base(_section, _url)
        {
            
        }

        public SimulatorSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }
}