using MbUnit.Framework;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Request.Payment
{
    [TestFixture]
    public class SimulatorSageRequest
    {

        public static readonly ResponseStatus[] ValidResponses = new[]
                                         {
                                             ResponseStatus.OK,
                                             ResponseStatus.Registered,
                                             ResponseStatus.NotAuthed,
                                             ResponseStatus.Rejected,
                                             ResponseStatus.Error
                                         };

        [TestFixture]
        internal class Send
        {
            [Test]
            [Repeat(100)]
            public void AValidTransactionReturnsAValidResponse()
            {
                var request = new SagePay.Request.Payment.SimulatorSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();

                request.Validate();

                var response = request.Send();


                Assert.Contains(ValidResponses, response.Status, 
                    string.Format("Error occured for status: {0}, message: {1}", 
                        response.Status,
                        response.StatusDetail));
            }
        }
    }
}