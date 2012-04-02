namespace OrangeTentacle.SagePay.Request.Payment
{
    public class TestSageRequest : WebSageRequest
    {
        private const string _url = "https://test.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Test;

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