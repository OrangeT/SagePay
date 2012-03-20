namespace OrangeTentacle.SagePay
{
    public class TestSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/gateway/service/vspdirect-register.vs";
        private const SagePayFactory.ProviderTypes _type = SagePayFactory.ProviderTypes.Test;

        public TestSageRequest()
            : base(_type, _url)
        {
        }

        public TestSageRequest(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }
}