using System.Collections.Generic;

namespace OrangeTentacle.SagePay.Request
{
    public interface IValidate
    {
        List<ValidationError> Validate();
    }
}