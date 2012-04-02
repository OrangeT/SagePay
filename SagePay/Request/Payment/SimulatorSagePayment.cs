namespace OrangeTentacle.SagePay.Request.Payment
{
    public class SimulatorSagePayment : WebSagePayment
    {
        private const string _url = "https://test.sagepay.com/Simulator/VSPDirectGateway.asp";
        private const ProviderTypes _type = ProviderTypes.Simulator;

        public SimulatorSagePayment()
            : base(_type, _url)
        {
            
        }

        public SimulatorSagePayment(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }

}