using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrangeTentacle.SagePayTest.Response
{
    
    public class FakeResponse
    {
        [Fact]
        public void Serialize()
        {
            // From SageDocs - response are in form:
            // Name=Value/r/n

            var fakeResponse = new FakePaymentTextResponse();
            fakeResponse.Collection = new Dictionary<string,string>();
            fakeResponse.Collection.Add("Test1", "TestValue1");
            fakeResponse.Collection.Add("Test2", "TestValue2");

            var expected = "Test1=TestValue1\r\nTest2=TestValue2";
            Assert.Equal(expected, fakeResponse.Write());
        }
    }

    public class FakePaymentTextResponse : FakeTextResponse
    {
        public FakePaymentTextResponse()
        {
            Collection = new Dictionary<string, string>();
            Collection.Add("VPSProtocol", "2.23");
            Collection.Add("Status", "OK");
            Collection.Add("StatusDetail", "Lorem Ipsum");
            Collection.Add("VPSTxId", Guid.NewGuid().ToString());
            Collection.Add("SecurityKey", "12341231aa");
            Collection.Add("TxAuthNo", "43152123141231231");
            Collection.Add("AVSCV2", "ALL MATCH");
            Collection.Add("AddressResult", "MATCHED");
            Collection.Add("PostCodeResult", "MATCHED");
            Collection.Add("CV2Result", "MATCHED");
            Collection.Add("3DSecureStatus", "OK");
            Collection.Add("CAVV", "123asdasdereasdawew");
        }
    }

    public class FakeRefundTextResponse : FakeTextResponse
    {
        public FakeRefundTextResponse()
        {
            Collection = new Dictionary<string, string>();
            Collection.Add("VPSProtocol", "2.23");
            Collection.Add("Status", "OK");
            Collection.Add("StatusDetail", "Lorem Ipsum");
            Collection.Add("VPSTxId", Guid.NewGuid().ToString());
            Collection.Add("TxAuthNo", long.MaxValue.ToString());
        }
    }

    public class FakeTextResponse
    {
        public Dictionary<string, string> Collection { get; set; }

        public string Write()
        {
            var col = (from pair in Collection
                       select string.Format("{0}={1}", pair.Key, pair.Value))
                          .ToArray();

            return string.Join("\r\n", col);
        }
    }
}