using System.Text;
using System.Collections.Generic;

namespace OrangeTentacle.SagePay
{
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public string PrettyPrint(IEnumerable<ValidationError> errors)
        {
            var sb = new StringBuilder();
            foreach(var error in errors)
                sb.AppendLine(string.Format("{0} - {1}", error.Field, error.Message));

            return sb.ToString();
        }
    }
}
