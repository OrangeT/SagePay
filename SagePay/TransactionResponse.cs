namespace OrangeTentacle.SagePay
{
    public class TransactionResponse
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

        public enum ResponseStatus
        {
            OK,
            Malformed,
            Invalid,
            NotAuthed,
            Rejected,
            ThreeDAuth,
            Ppredirect,
            Authenticated,
            Registered,
            Error
        }

        public enum CV2Status
        {
            AllMatch,
            SecurityCodeMatchOnly,
            AddressMatchOnly,
            NoDataMatches,
            DataNotChecked
        }

        public enum MatchStatus
        {
            NotProvided,
            NotChecked,
            Matched,
            NotMatched
        }

        public enum ThreeDSecureStatus
        {
            OK,
            NoAuth,
            CanAuth,
            NotAuthed,
            AttemptOnly,
            NotChecked,
            Incomplete,
            Malformed,
            Invalid,
            Error
        }
    }


}