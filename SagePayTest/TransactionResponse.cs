using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest
{
    [TestFixture]
    public class TransactionResponse
    {
        [TestFixture]
        internal class IsValid
        {
            [Test]
            public void StatusOk()
            {
                var response = new SagePay.TransactionResponse();
                response.Status = SagePay.TransactionResponse.ResponseStatus.OK;

                Assert.IsTrue(response.IsValid());
            }

            [Test]
            public void StatusElse([EnumData(typeof(SagePay.TransactionResponse.ResponseStatus), 
                Exclude = SagePay.TransactionResponse.ResponseStatus.OK)] SagePay.TransactionResponse.ResponseStatus status)
            {
                var response = new SagePay.TransactionResponse();
                response.Status = status;

                Assert.IsFalse(response.IsValid());
            }

        }
    }
}