using MbUnit.Framework;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Response
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
                var response = new SagePay.Response.PaymentResponse();
                response.Status = SagePay.Response.ResponseStatus.OK;

                Assert.IsTrue(response.IsValid());
            }

            [Test]
            public void StatusElse([EnumData(typeof(ResponseStatus), 
                Exclude = ResponseStatus.OK)] ResponseStatus status)
            {
                var response = new SagePay.Response.PaymentResponse();
                response.Status = status;

                Assert.IsFalse(response.IsValid());
            }

        }
    }
}