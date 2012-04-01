namespace OrangeTentacle.SagePay.Response
{
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
}