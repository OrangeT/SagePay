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
                var request = new SagePay.SimulatorSageRequest();
                request.Transaction = TransactionRequest.SampleRequest();

                request.Validate();

                var response = request.Send();

                var validResponses = new[]
                                         {
                                             SagePay.TransactionResponse.ResponseStatus.OK,
                                             SagePay.TransactionResponse.ResponseStatus.Registered,
                                             SagePay.TransactionResponse.ResponseStatus.NotAuthed,
                                             SagePay.TransactionResponse.ResponseStatus.Rejected,
                                             SagePay.TransactionResponse.ResponseStatus.Error
                                         };

                Assert.Contains(validResponses, response.Status, 
                    string.Format("Error occured for status: {0}, message: {1}", 
                        response.Status,
                        response.StatusDetail));
            }
        }
    }
}