namespace OrangeTentacle.SagePay.Forms
{
    public class TestSageForm : SageForm
    {
        private const string _url = "https://live.sagepay.com/gateway/service/vspdirect-register.vsp";
        private const ProviderTypes _type = ProviderTypes.Live;

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
