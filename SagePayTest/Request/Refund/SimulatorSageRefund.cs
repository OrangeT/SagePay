﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using OrangeTentacle.SagePay.Response;

namespace OrangeTentacle.SagePayTest.Request.Refund
{
    [TestFixture]
    class SimulatorSageRefund
    {
        public static readonly ResponseStatus[] ValidResponses = new[]
                                         {
                                             ResponseStatus.OK,
                                             ResponseStatus.Registered,
                                             ResponseStatus.NotAuthed,
                                             ResponseStatus.Rejected,
                                             ResponseStatus.Error
                                         };

        [Test]
        [Repeat(100)]
        public void AValidTransactionReturnsAValidResponse()
        {
            var request = new SagePay.Request.Refund.SimulatorSageRefund();
            request.Transaction = RefundRequest.SampleRequest();

            request.Validate();

            var response = request.Send();

            Assert.Contains(ValidResponses, response.Status,
                string.Format("Error occured for status: {0}, message: {1}",
                    response.Status,
                    response.StatusDetail));
        }
    }
}