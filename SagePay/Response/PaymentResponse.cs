namespace OrangeTentacle.SagePay.Response
{
    public class PaymentResponse
    {
        public string VPSProtocol { get { return "2.23"; } }
        public ResponseStatus Status { get; set; }
        public string StatusDetail { get; set; }
        public string VPSTxId { get; set; }
        public string SecurityKey { get; set; }
        public long TxAuthNo { get; set; }
        public CV2Status AVSCV2 { get; set; }
        public MatchStatus AddressResult { get; set; }
        public MatchStatus PostCodeResult { get; set; }
        public MatchStatus CV2Result { get; set; }
        public ThreeDSecureStatus ThreeDSecure { get; set; }
        public string Caav { get; set; }

        public bool IsValid()
        {
            return Status == ResponseStatus.OK;
        }
    }
}