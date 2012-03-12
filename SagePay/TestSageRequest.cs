namespace OrangeTentacle.SagePay
{
    public class TestSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/gateway/service/vspdirect-register.vs";
        private const string _section = "TestSagePay";

        public TestSageRequest()
            : base(_section, _url)
        {
        }

        public TestSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }
}