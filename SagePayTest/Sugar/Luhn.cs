using MbUnit.Framework;

namespace OrangeTentacle.SagePayTest.Sugar
{
    [TestFixture]
    public class Luhn
    {
        [TestFixture]
        internal class IsValid
        {
            [Test]
            public void NonNumeric()
            {
                var number = "79927398713a";
                Assert.IsFalse(SagePay.Sugar.Luhn.IsValid(number));
            }

            [Test]
            public void Valid()
            {
                var number = "79927398713";
                Assert.IsTrue(SagePay.Sugar.Luhn.IsValid(number));                
            }

            [Test]
            public void InValid()
            {
                var number = "79927398723";
                Assert.IsFalse(SagePay.Sugar.Luhn.IsValid(number));
            }

            [Test]
            [Factory(typeof(SampleCard), "AllCards")]
            public void AllCardsAreValid(SampleCard card)
            {
                Assert.IsTrue(SagePay.Sugar.Luhn.IsValid(card.Number), 
                              string.Format("{0} card does not validate", card.CardType));
            }
        }
    }
}