using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    // Skip

    //public class SimulatorSageRefund
    //{
    //    public static readonly ResponseStatus[] ValidResponses = new[]
    //                                     {
    //                                         ResponseStatus.OK,
    //                                         ResponseStatus.Registered,
    //                                         ResponseStatus.NotAuthed,
    //                                         ResponseStatus.Rejected,
    //                                         ResponseStatus.Error
    //                                     };

    //    [Fact]
    //    public void AValidTransactionReturnsAValidResponse()
    //    {
    //        for (int i = 0; i < 100; i++)
    //        {
    //            var request = new SagePay.Request.Refund.SimulatorSageRefund();
    //            request.Transaction = RefundRequest.SampleRequest();

    //            request.Validate();

    //            var response = request.Send();

    //            Assert.Contains(response.Status, ValidResponses);
    //        }
    //    }
    //}
}
