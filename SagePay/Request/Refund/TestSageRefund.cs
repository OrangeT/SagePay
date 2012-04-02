namespace OrangeTentacle.SagePay.Request.Refund
{
    public class TestSageRefund : WebSageRefund
    {
        private const string _url = "https://test.sagepay.com/gateway/service/refund.vsp";
        private const ProviderTypes _type = ProviderTypes.Test;

        public TestSageRefund()
            : base(_type, _url)
        {

        }

        public TestSageRefund(string vendorName)
            : base(vendorName, _url)
        {

        }

    }
}