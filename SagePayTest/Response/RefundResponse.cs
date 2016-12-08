using Xunit;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Response
{
    
    public class RefundResponse
    {
        
        public class IsValid
        {
            [Fact]
            public void StatusOk()
            {
                var response = new SagePay.Response.RefundResponse();
                response.Status = ResponseStatus.OK;

                Assert.True(response.IsValid());
            }

            [Theory]
            [InlineData(ResponseStatus.Invalid)]
            [InlineData(ResponseStatus.Malformed)]
            [InlineData(ResponseStatus.NotAuthed)]
            [InlineData(ResponseStatus.Ppredirect)]
            [InlineData(ResponseStatus.Registered)]
            [InlineData(ResponseStatus.Rejected)]
            [InlineData(ResponseStatus.ThreeDAuth)]
            [InlineData(ResponseStatus.Authenticated)]
            public void StatusElse(ResponseStatus status)
            {
                var response = new SagePay.Response.RefundResponse();
                response.Status = status;

                Assert.False(response.IsValid());
            }
        }
    }
}