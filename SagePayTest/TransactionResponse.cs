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
                var response = new SagePay.Response.TransactionResponse();
                response.Status = SagePay.Response.ResponseStatus.OK;

                Assert.IsTrue(response.IsValid());
            }

            [Test]
            public void StatusElse([EnumData(typeof(SagePay.Response.ResponseStatus), 
                Exclude = SagePay.Response.ResponseStatus.OK)] SagePay.Response.ResponseStatus status)
            {
                var response = new SagePay.Response.TransactionResponse();
                response.Status = status;

                Assert.IsFalse(response.IsValid());
            }

        }
    }
}