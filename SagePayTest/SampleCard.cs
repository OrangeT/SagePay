using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrangeTentacle.SagePay.Request;
using OrangeTentacle.SagePay.Request.Payment;

namespace OrangeTentacle.SagePayTest
{
    // Sample Cards from SAGE Documentation (11/03/2012)
    public class SampleCard
    {
        public const string VISA = "4929000000006";
        public const string DELTA = "4462000000000003";
        public const string UKE = "4917300000000008";
        public const string MC = "5404000000000001";
        public const string UK_MAESTRO = "5641820000000005";
        public const string INTL_MAESTRO = "300000000000000004";
        public const string AMEX = "374200000000004";
        public const string JCB = "3569990000000009";
        public const string DC = "36000000000008";
        public const string LASER = "6304990000000000044";

        public const string CV2 = "123";
        public const string BILLING_ADDRESS = "88";
        public const string POSTCODE = "412";

        public CardType CardType { get; set; }
        public string Number { get; set; }

        public static List<SampleCard> AllCards()
        {
            return new List<SampleCard>
                {
                    new SampleCard {CardType = CardType.Visa, Number = VISA},
                    new SampleCard {CardType = CardType.Delta, Number = DELTA},
                    new SampleCard {CardType = CardType.Uke, Number = UKE},
                    new SampleCard {CardType = CardType.Mc, Number = MC},
                    new SampleCard {CardType = CardType.Maestro, Number = UK_MAESTRO},
                    new SampleCard {CardType = CardType.Maestro, Number = INTL_MAESTRO},
                    new SampleCard {CardType = CardType.Amex, Number = AMEX},
                    new SampleCard {CardType = CardType.Jcb, Number = JCB},
                    new SampleCard {CardType = CardType.Dc, Number = DC},
                    new SampleCard {CardType = CardType.Laser, Number = LASER}
                };
        }

    }
}
