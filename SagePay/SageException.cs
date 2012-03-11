using System;

namespace OrangeTentacle.SagePay
{
    public class SageException : Exception
    {
        public SageException()
        {}

        public SageException(string message) : base(message)
        {}
    }
}