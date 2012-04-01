using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class SimulatorSageRequest
    {
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

                var validResponses = new[]
                                         {
                                             SagePay.Response.ResponseStatus.OK,
                                             SagePay.Response.ResponseStatus.Registered,
                                             SagePay.Response.ResponseStatus.NotAuthed,
                                             SagePay.Response.ResponseStatus.Rejected,
                                             SagePay.Response.ResponseStatus.Error
                                         };

                Assert.Contains(validResponses, response.Status, 
                    string.Format("Error occured for status: {0}, message: {1}", 
                        response.Status,
                        response.StatusDetail));
            }
        }
    }
}