using Xunit;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    
    public class SimulatorSagePayment
    {

        public static readonly ResponseStatus[] ValidResponses = new[]
                                         {
                                             ResponseStatus.OK,
                                             ResponseStatus.Registered,
                                             ResponseStatus.NotAuthed,
                                             ResponseStatus.Rejected,
                                             ResponseStatus.Error
                                         };

        
        public class Send
        {
            /* Please note - IP address needs to be valid with SagePay */

            [Fact]
            public void AValidTransactionReturnsAValidResponse()
            {
                for (var i = 0; i < 100; i++)
                {
                    var request = new SagePay.Request.Payment.SimulatorSagePayment();
                    request.Transaction = PaymentRequest.SampleRequest();

                    request.Validate();

                    var response = request.Send();

                    Assert.Contains(response.Status, ValidResponses);
                }
            }
        }
    }
}