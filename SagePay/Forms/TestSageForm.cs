namespace OrangeTentacle.SagePay.Forms
{
    public class TestSageForm : SageForm
    {
        public TestSageForm(ProviderTypes type) : base(type)
        {
            SetUrl();
        }

        public TestSageForm(string vendorName, string encodeKey) : base(vendorName, encodeKey)
        {
            SetUrl();
        }

        private void SetUrl()
        {
            Url = "https://test.sagepay.com/gateway/service/vspform-register.vsp";
        }
    }
}