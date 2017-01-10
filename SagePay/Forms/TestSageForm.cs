namespace OrangeTentacle.SagePay.Forms
{
    public class TestSageForm : SageForm
    {
        private const string _url = "https://test.sagepay.com/gateway/service/vspform-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Test;

        public TestSageForm() 
            : base(_type, _url)
        {
        }

        public TestSageForm(string vendorName, string encodeKey) 
            : base(vendorName, encodeKey, _url)
        {
        }
    }
}
