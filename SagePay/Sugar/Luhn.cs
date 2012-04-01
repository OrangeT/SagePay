using System;

namespace OrangeTentacle.SagePay.Sugar
{
    public class Luhn
    {
        /// <summary>
        /// Performs a Luhn checksum on a credit/debit card number.
        /// </summary>
        public static bool IsValid(string number)
        {
            var reversed = number.ToCharArray();
            Array.Reverse(reversed);

            var sum = 0;
            for (int i = 0; i < number.Length; i++ )
            {
                var result = char.GetNumericValue(reversed[i]);
                if (result == -1)
                    return false;

                if (i % 2 == 0)
                    sum += (int)result;
                else
                {
                    result = result*2;
                    var tens = (int) (result/10);
                    result = tens + result - (tens * 10);
                    sum += (int)result;
                }
            }
            return sum % 10 == 0;
        }
    }
}