using System.Reflection;
using Xunit;

namespace OrangeTentacle.SagePayTest.Sugar
{
    
    public class Luhn
    {
        
        public class IsValid
        {
            [Fact]
            public void NonNumeric()
            {
                var number = "79927398713a";
                Assert.False(SagePay.Sugar.Luhn.IsValid(number));
            }

            [Fact]
            public void Valid()
            {
                var number = "79927398713";
                Assert.True(SagePay.Sugar.Luhn.IsValid(number));                
            }

            [Fact]
            public void InValid()
            {
                var number = "79927398723";
                Assert.False(SagePay.Sugar.Luhn.IsValid(number));
            }

            [Theory, MemberData("AllCards", MemberType = typeof(SampleCard))]
            public void AllCardsAreValid(SampleCard card)
            {
                Assert.True(SagePay.Sugar.Luhn.IsValid(card.Number), 
                              string.Format("{0} card does not validate", card.CardType));
            }
        }
    }
}
