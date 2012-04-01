namespace OrangeTentacle.SagePay.Response
{
    public enum ThreeDSecureStatus
    {
        OK,
        NoAuth,
        CantAuth,
        NotAuthed,
        AttemptOnly,
        NotChecked,
        Incomplete,
        Malformed,
        Invalid,
        Error
    }
}