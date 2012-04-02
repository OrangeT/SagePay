namespace OrangeTentacle.SagePay.Request.Payment
{
    public class TestSagePayment : WebSagePayment
    {
        private const string _url = "https://test.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Test;

        public TestSagePayment()
            : base(_type, _url)
        {
        }

        public TestSagePayment(string vendorName)
            : base(vendorName, _url, true)
        {
            
        }
    }
}