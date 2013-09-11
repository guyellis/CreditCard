using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Company.CreditCard.UnitTest
{
    public class CreditCardDerived : CreditCard
    {
        // The functions in this class wrap the private (protected) methods in the CreditCard
        // class so that unit testing can be done against each of these functions.

        public bool PassesCardType_ForUnitTest(string cardNumber)
        {
            return PassesCardType(cardNumber);
        }
        public string Reverse_ForUnitTest(string text)
        {
            return Reverse(text);
        }
        public bool CharIsHex_ForUnitTest(char c)
        {
            return CharIsHex(c);
        }
        public int CountConsecutiveHexChar_ForUnitTest(string text)
        {
            return CountConsecutiveHexChar(text);
        }

        public bool NotInGuid_ForUnitTest(int ccIndex, int ccLength, List<Capture> guidList)
        {
            return NotInGuid(ccIndex, ccLength, guidList);
        }

        public List<Capture> GetGUIDList_ForUnitTest(string text)
        {
            return GetGUIDList(text);
        }

        public bool PassesCreditCardSanityCheck_ForUnitTest(string cardNumber)
        {
            return PassesCreditCardSanityCheck(cardNumber);
        }

        public string TrimNotDigits_ForUnitTest(string text)
        {
            return TrimNonDigits(text);
        }
    }
}
