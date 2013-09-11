using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Collections;

namespace Company.CreditCard.UnitTest
{
    /// <summary>
    /// Unit tests for Credit Card library
    /// </summary>
    [TestClass]
    public class TestCreditCard
    {
        #region Tool generated code
        public TestCreditCard()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion Tool generated code

        [TestMethod]
        public void Test_a_specific_number()
        {
            // Use this unit test to plug in specific numbers that you suspect aren't working
            CreditCard cc = new CreditCard();
            string text = "34173 045716 6171";
            int expectedCount = 1;

            Assert.AreEqual(expectedCount, cc.CreditCardNumbersInText(text).Count);
        }

        [TestMethod]
        public void Test_a_set_of_one_per_line_thirteen_digit_visa_numbers()
        {
            CreditCard cc = new CreditCard();
            string text = "VISA 13 digit\n\n4716 146999146\n4916540 642025\n4716810 234929\n492909659 2916\n497600059 3985";
            Assert.AreEqual(5, cc.CreditCardNumbersInText(text).Count);
        }

        [TestMethod]
        public void Test_a_set_of_Visa_numbers()
        {
            CreditCard cc = new CreditCard();
            string text = "Here are a bunch of thirteen digit visa numbers: 4642586703402 and 4916612826126 and 4556204160730 and 4485447739342 and 4024007164037";

            Assert.AreEqual(5, cc.CreditCardNumbersInText(text).Count);
        }

        [TestMethod]
        public void Test_a_set_of_Amex_numbers()
        {
            CreditCard cc = new CreditCard();
            string text = "Here are a bunch of Amex numbers: 371761891034009 and 344597253435523 and 341730457166171 and 372757216941380 and 3493 705545 00329";

            Assert.AreEqual(5, cc.CreditCardNumbersInText(text).Count);
        }

        [TestMethod]
        public void Test_find_credit_card_numbers_in_text_block()
        {
            string text = "Here is my credit card: 6011 1111 1111 1117 and here is another one that I have: 4111-1111-1111-1111 and this one looks like a credit card but is not: 4111-1111-1111-111 and this one is an invalid credit card number 1234-4567-8901-2345 and this one is an invalid amex number 1234-123456-12345";
            CreditCard cc = new CreditCard();
            List<Capture> ccLocations = cc.CreditCardNumbersInText(text);
            Assert.AreEqual(2, ccLocations.Count);
            Assert.AreEqual("6011 1111 1111 1117", ccLocations[0].Value.Trim());
            Assert.AreEqual("4111-1111-1111-1111", ccLocations[1].Value.Trim());

            text = "";
            ccLocations = cc.CreditCardNumbersInText(text);
            Assert.AreEqual(0, ccLocations.Count);

            text = String.Empty;
            ccLocations = cc.CreditCardNumbersInText(text);
            Assert.AreEqual(0, ccLocations.Count);
        }

        [TestMethod]
        public void Test_that_part_of_credit_card_number_is_replaced_with_stars()
        {
            CreditCard cc = new CreditCard();
            string text = "Here is my credit card: 6011 1111 1111 1117 and ";
            string textExpected = "Here is my credit card: ************** 1117 and ";
            Assert.IsTrue(cc.ReplaceAllButLastFourWithStars(ref text));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011-1111-1111-1117 and ";
            textExpected = "Here is my credit card: **************-1117 and ";
            Assert.IsTrue(cc.ReplaceAllButLastFourWithStars(ref text));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011 1111 1111 1117 and ";
            textExpected = "Here is my credit card: ******************* and ";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.AreEqual(textExpected, text);
        }

        [TestMethod]
        public void Test_that_part_of_credit_card_number_is_replaced_with_specified_character()
        {
            CreditCard cc = new CreditCard();

            string text = "Here is my credit card: 6011 1111 1111 1117 and ";
            string textExpected = "Here is my credit card: ------------11 1117 and ";
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref text, '-', 6));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011 1111 1111 1117 and ";
            textExpected = "Here is my credit card: -----------------17 and ";
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref text, '-', 2));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011 1111 1111 1117 and ";
            textExpected = "Here is my credit card: -----------111 1117 and ";
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref text, '-', 7));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011 1111 1111 1117 and ";
            textExpected = "Here is my credit card: --------1 1111 1117 and ";
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref text, '-', 9));
            Assert.AreEqual(textExpected, text);

            text = "Here is my credit card: 6011 1111 1111 1117 and ";
            textExpected = "Here is my credit card: ------------------- and ";
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref text, '-', 0));
            Assert.AreEqual(textExpected, text);
        }

        [TestMethod]
        public void Test_html_in_email()
        {
            string htmlInEmail = "<html><body><FONT face=verdana color=#000000 size=2>\n" +
                "<div><FONT face=verdana,geneva color=#000000>visa&nbsp; 4485143748437070 </FONT></div>\n" +
                "<div>&nbsp;</div>\n" +
                "<div>Thanks,</div>\n" +
                "<div>&nbsp;</div>\n" +
                "<div>Some Name</div>\n" +
                "<div>Rocket Engineer - Internal Services</div>\n" +
                "<div>The Company Group</div>\n" +
                "<div>14455 N. Peach Rd.</div>\n" +
                "<div>Suite 219</div>\n" +
                "<div>Shakesville, AZ 85260</div>\n" +
                "<div>480-555-5555 Ext. 4274</div></FONT></body></html>\n";

            CreditCard cc = new CreditCard();
            Assert.IsTrue(cc.ReplaceAllWithStars(ref htmlInEmail));
            Assert.IsFalse(htmlInEmail.Contains("4485143748437070"));
        }

        [TestMethod]
        public void Test_ssl_number()
        {
            string html = "<html><body><font faceDear Secure Certificate Customer,<br><br>We have received a Request for the following product: soap.<br><br>Our query of the  database returned your name as the administrator for the purchase in the purchase request.<br><br>In order to verify the validity of this request and that it was submitted by the entity to which the soap in the request is registered, please signify your final approval or disapproval of the soap request by clicking the link below.<br><br><a ://soapy.com/Verify.got?vk=24058114907c4ad68e1011bf7a667d912ece9424395741892056626176soapy.com/Verify.got?vk=24058114907c4ad68e1011bf7a667d912ece9424395741892056626176</a><br><br>Approval of the request will enable us to continue processing your request. Failure to approve the soap request will lead to denial of the request.<br><br>If the above address does not appear as a clickable link, cut/copy and paste it into your browser's address bar.<br><br>If the Verification Page requests it, please use the following Verification Key:<b> 24058114907c4ad68e1011bf7a667d912ece9424395741892056626176 </b><br><br>If you encounter any problems or have any questions, our Customer Support department is ready to help, around-the-clock, seven days a week.<br><br>Customer Support:<br>E-Mail: myself@soapy.com<br>Phone:  480.555.5555<br>Fax: 480.555.5555<br><br>For further information, log in to your soap account at: https://soapy.com.<br></font></body></html>";

            CreditCard cc = new CreditCard();
            Assert.IsFalse(cc.ReplaceAllWithStars(ref html));
        }

        [TestMethod]
        public void Test_actual_guid_that_got_masked_in_iris()
        {
            // A GUID pattern is a series of 32 hexadecimal digits with a pattern of 8-4-4-4-12
            // e.g.: e3d6486f-8169-4674-9382-55a013edfb43
            //                -----------------
            // The underlined characters above form a valid credit card number. We need to check
            // that any valid credit card number inside a GUID is not picked up as a credit card
            // number.
            string html = "<b><font color=\"blue\">Product add</font></b><br>Please configure the following product for distribution:<div style=\"margin-left:10px; margin-right:10px; border:1px; border-style:solid; background: #FFFF00\"><font color=\"blue\"><b>Plan Type: </b>2 - Daily for 30 Days</font> <b>GUID: </b>e3d6486f-8169-4674-9382-55a013edfb43 <b>side bar: </b>yes if big<b>";
            CreditCard cc = new CreditCard();
            Assert.IsFalse(cc.ReplaceAllWithStars(ref html));
        }

        [TestMethod]
        public void Test_that_guids_dont_get_masked()
        {
            CreditCard cc = new CreditCard();
            string html = "e3d6486f-8169-4674-9382-55a013edfb43";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref html), String.Format("This guid ({0}) should not have been masked", html));
        }

        [TestMethod]
        public void Test_that_PassesLuhnTest_returns_false_with_invalid_input()
        {
            CreditCard cc = new CreditCard();
            Assert.IsFalse(cc.PassesLuhnTest(null));
        }

        [TestMethod]
        public void Test_one_digit_start_and_end()
        {
            string html = "masked - 1 digit on end 45566989040852151\n\n" +
                "masked - 1 digit at beginning 14556698904085215";
            CreditCard cc = new CreditCard();
            // Confirm that nothing is found.
            Assert.IsFalse(cc.ReplacePartOfNumbers(ref html, '*', 6));
            Assert.IsFalse(html.Contains("*****"));
        }

        [TestMethod]
        public void Test_one_digit_start_and_end_whole_block_in_html()
        {
            string html = "<html><body><span style=\"font-family:Verdana; color:#000000; font-size:10pt;\">masked - plain cc 4556698904085215<br><br>masked - textatstart4556698904085215<br><br>masked - textatstart4556698904085215textatend<br><br>not masked - 4556698textinmiddle904085215<br><br>masked - 1 digit on end 45566989040852151<br><br>masked - 1 digit at beginning 14556698904085215<br><br>not masked - 1 digit on end with spaces 4556 6989 0408 52151<br><br>not masked - 1 digit at beginning with spaces 1455 6698 9040 85215<br><br>not masked - 1 digit on end with dash 4556-6989-0408-52151<br><br>not masked - 1 digit at beginning with dash 1455-6698-9040-85215<br><br>Some Name<br>Some Company<br>480.555.5555 x4767</span></body></html>";

            CreditCard cc = new CreditCard();
            // Confirm that nothing is found.
            Assert.IsTrue(cc.ReplacePartOfNumbers(ref html, '*', 6));
            // Confirm that CC's with extra digit are not marked as CC's.
            Assert.IsTrue(html.Contains("45566989040852151"));
            Assert.IsTrue(html.Contains("14556698904085215"));

        }

        [TestMethod]
        public void Test_that_passes_luhns_test()
        {
            // Get a list of credit card numbers from here:
            // http://www.darkcoding.net/credit-card-numbers/
            string[] validCreditCards = { "4111-1111-1111-1111", "3782-822463-10005", "5105-1051-0510-5100", "6011-1111-1111-1117" };

            CreditCard cc = new CreditCard();
            foreach (string s in validCreditCards)
            {
                Assert.IsTrue(cc.PassesLuhnTest(s), String.Format("PassesLuhnTest should return true for {0}", s));
            }
            string[] invalidCreditCards = { "4111-0000-0000-0000", "4321-4321-4321-4321" };

            foreach (string s in invalidCreditCards)
            {
                Assert.IsFalse(cc.PassesLuhnTest(s), String.Format("PassesLuhnTest should return false for {0}", s));
            }
        }

        [TestMethod]
        public void Test_credit_card_sanity_check()
        {
            string[] validCreditCards = { "4111-1111-1111-1111", "3782-822463-10005", "5105-1051-0510-5100", "6011-1111-1111-1117" };

            CreditCardDerived cc = new CreditCardDerived();
            foreach (string s in validCreditCards)
            {
                Assert.IsTrue(cc.PassesCreditCardSanityCheck_ForUnitTest(s), String.Format("Should pass sanity check but fails for {0}", s));
            }
            string[] invalidCreditCards = { "4111-0000-0000-0000-1234", "0000-0000-0000-0000", "0000-0000-0000", "4321-4321-43Z1-4321" };

            foreach (string s in invalidCreditCards)
            {
                Assert.IsFalse(cc.PassesCreditCardSanityCheck_ForUnitTest(s), String.Format("Should fail sanity check but passes for {0}", s));
            }
        }

        [TestMethod]
        public void Test_text_with_verifycsr_verification_key()
        {
            #region input data
            string html = "Somebody,\n I 'approved' this.....so you should be good to go....Someone else\n ----- Original Message ----- \n From: x@domain.com \n To: customer@domain.com \n Sent: Thursday, Juliary 03, 2002 3:39 PM\n Subject: Soap Purchase\n\n\n Dear Customer,\n\n We have received a Soap Request for the following product: valentine soap.\n\n Our query of the database returned your name as the administrator for the soap in the request.\n\n In order to verify the validity of this request and that it was submitted by the entity to which the soap in the request is registered, please signify your final approval or disapproval of the product request by clicking the link below.\n\n https://domain.com/Verify.got?vk=83782207101a742392127107484d25f111c31042294274238333031\n\n Approval of the request will enable us to continue processing your request. Failure to approve the certificate request will lead to denial of the request.\n\n If the above address does not appear as a clickable link, cut/copy and paste it into your browser's address bar.\n\n If the Verification Page requests it, your Verification Key is here: 83782207101a742392127107484d25f111c31042294274238333031 \n\n If you encounter any problems or have any questions, our Customer Support department is ready to help, around-the-clock, seven days a week.\n\n Customer Support:\n E-Mail: x@domain.com\n Phone: 480.555.5555\n Fax: 480.555.5555\n\n For further information, log in to your soap account at: https://domain.com.";
            #endregion input data

            CreditCard cc = new CreditCard();
            // Confirm that nothing is found.
            Assert.IsFalse(cc.ReplaceAllWithStars(ref html));
        }

        [TestMethod]
        public void Test_consecutive_hex_characters()
        {
            CreditCardDerived cc = new CreditCardDerived();
            Assert.AreEqual<int>(0, cc.CountConsecutiveHexChar_ForUnitTest("xyz"));
            Assert.AreEqual<int>(3, cc.CountConsecutiveHexChar_ForUnitTest("abc"));
            Assert.AreEqual<int>(6, cc.CountConsecutiveHexChar_ForUnitTest("abcdefghi"));
            Assert.AreEqual<int>(16, cc.CountConsecutiveHexChar_ForUnitTest("1234567890abcdefghi"));
            Assert.AreEqual<int>(16, cc.CountConsecutiveHexChar_ForUnitTest("1234567890abcdefghi".ToUpper()));
            Assert.AreEqual<int>(5, cc.CountConsecutiveHexChar_ForUnitTest("12345xyz67890abcdefghi"));
        }

        [TestMethod]
        public void Test_string_reverse_function()
        {
            CreditCardDerived cc = new CreditCardDerived();
            Assert.AreEqual<string>("fedcba", cc.Reverse_ForUnitTest("abcdef"));
        }

        [TestMethod]
        public void Test_with_adjacent_hex_values()
        {
            CreditCard cc = new CreditCard();
            const string validCC = "4111111111111111";

            string text = validCC;
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = "d" + validCC;
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = "de" + validCC;
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = "def" + validCC;
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = "de" + validCC + "a";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = "d" + validCC + "ea";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = validCC + "e";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = validCC + "ea";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            text = validCC + "eaf";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));

            text = validCC + "defa";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
            text = "d" + validCC + "aae";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
            text = "de" + validCC + "aa";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
            text = "ade" + validCC + "a";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
            text = "defa" + validCC;
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
        }

        [TestMethod]
        public void Test_card_types()
        {
            // The PassesCardType function tests the first few digits of a credit card to make sure
            // that the length matches a valid card type.

            // The following card numbers do not need to be valid "Luhn" card numbers but just have
            // to be the correct length and start with the correct numbers to pass this test.
            List<string> validNumbers = new List<string>();
            List<string> invalidNumbers = new List<string>();
            // CARD TYPE                   Prefix              Length
            // VISA                        4                   13
            validNumbers.Add("4234567890123"); 
            invalidNumbers.Add("1234567890123");
            // Diners Club/Carte Blanche   300-305, 36, 38     14
            validNumbers.Add("30045678901234");
            validNumbers.Add("36345678901234");
            validNumbers.Add("38345678901234");
            validNumbers.Add("30045678901234");
            invalidNumbers.Add("10545678901234");
            // enRoute                     2014, 2149          15
            validNumbers.Add("201456789012345");
            validNumbers.Add("214956789012345");
            invalidNumbers.Add("801456789012345");
            // AMEX                        34, 37              15
            validNumbers.Add("341456789012345");
            validNumbers.Add("371456789012345");
            invalidNumbers.Add("311456789012345");
            // JCB                         2131, 1800          15
            validNumbers.Add("213156789012345");
            validNumbers.Add("180056789012345");
            // Discover                    6011                16
            // JCB                         3                   16
            // MASTERCARD                  51-55               16
            // VISA                        4                   16
            validNumbers.Add("6011567890123456");
            validNumbers.Add("3014567890123456");
            validNumbers.Add("5114567890123456");
            validNumbers.Add("5514567890123456");
            validNumbers.Add("4234567890123456");
            invalidNumbers.Add("8514567890123456");
            invalidNumbers.Add("9514567890123456");
            invalidNumbers.Add("1514567890123456");
            invalidNumbers.Add("1234567890123456");

            // Voyager                    8699
            validNumbers.Add("869972808239529");
            invalidNumbers.Add("869872808239529");

            CreditCardDerived cc = new CreditCardDerived();
            foreach (string s in validNumbers)
            {
                Assert.IsTrue(cc.PassesCardType_ForUnitTest(s), s);
            }
            foreach (string s in invalidNumbers)
            {
                Assert.IsFalse(cc.PassesCardType_ForUnitTest(s), s);
            }
        }

        [TestMethod]
        public void Test_credit_card_email_count_with_hex()
        {
            string email = "This is a fake Mastercard number: 5300042628491142\n\nand this is the same number 5300042628491142abcd with four hex characters after it,\n\nand again with abcd5300042628491142 four hex characters before it, \n\nand again with ab5300042628491142cd with two hex before and after it.";
            CreditCardDerived ccd = new CreditCardDerived();
            List<Capture> captureList = ccd.CreditCardNumbersInText(email);
            Assert.AreEqual(1, captureList.Count);
        }

        [TestMethod]
        public void Test_credit_card_email_replacement_with_hex()
        {
            string email = "This is a fake Mastercard number: 5300042628491142\n\nand this is the same number 5300042628491142abcd with four hex characters after it,\n\nand again with abcd5300042628491142 four hex characters before it, \n\nand again with ab5300042628491142cd with two hex before and after it.";
            CreditCardDerived ccd = new CreditCardDerived();
            bool result = ccd.ReplaceAllWithStars(ref email);
            Assert.IsTrue(result);
            Assert.AreEqual(16, email.ToCharArray().Where(a => a == '*').Count());

            email = "5300042628491142";
            Assert.IsTrue(ccd.ReplaceAllWithStars(ref email));
            Assert.AreEqual(16, email.ToCharArray().Where(a => a == '*').Count());

            email = "5300042628491142xyz5300042628491134";
            Assert.IsTrue(ccd.ReplaceAllWithStars(ref email));
            Assert.AreEqual(32, email.ToCharArray().Where(a => a == '*').Count());
        }

        [TestMethod]
        public void Test_with_period_at_end_of_number()
        {
            string text = "This is a body of text containing visa cc 4485230168606183 and 4485230168606191. And the start of a new sentance.";
            CreditCardDerived ccd = new CreditCardDerived();
            Assert.AreEqual(2, ccd.CreditCardNumbersInText(text).Count);
        }

        [TestMethod]
        public void test_customer_use_case_that_failed_with_expiry_date_after_number()
        {
            string text = "customer is asking about domains to be renewed...stating that he had issues with getting cc info to accept..4111111111111111  03-11...pitched ddc..";

            CreditCard cc = new CreditCard();
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111111111111111"));
        }

        [TestMethod]
        public void test_customer_text_that_failed_2009_06_12()
        {
            string text =
                @"Hi,
At the moment I do not want to change my account email address to a new one,
so I would like if you can verify this email (fake@gmail.com). Here
are my details

*Full Name:*                Fake Faker
*Login Name:*                  99999999
*Receipt Number:              *999999999
*Customer Number:*          99999999
*Credit Card Number: *      4111111111111111 (This is which I used to
purchased the domain).
*PIN:                                      *999 (My CC provider didn't
provide me a 4 digit pin number).

By the wat if you are unable to verify my email as fake@gmail.com,
please look forward to change my email address to faler@gmail.com

Thanks";

            CreditCard cc = new CreditCard();
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111111111111111"));
        }

        [TestMethod]
        public void test_crm_text_that_failed_2009_10_05()
        {
            //             0        1
            //             1234567890123456
            string text = "4111111111111111, told her how to cancel products wrong cc.";

            CreditCard cc = new CreditCard();
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111111111111111"));
        }

        [TestMethod]
        public void test_comma_placement_after_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "hi im a customer and i need help removing credit card 411111111111-1111,, from my account.";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("411111111111-1111"));

            text = "hi im a customer and i need help removing credit card 411111111111-1111,from my account.";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("411111111111-1111"));
        }

        [TestMethod]
        public void sqaure_brackets_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with square brackets [4111-1111-1111-1111]";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void round_brackets_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with round brackets (4111-1111-1111-1111) and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void curly_brackets_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with braces {4111-1111-1111-1111} and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void curly_brackets_around_guid()
        {
            CreditCard cc = new CreditCard();

            string text = "guid with credit card at end of braces {37ed3389-9863-4e2e-4111-111111111111} and ...";
            Assert.IsFalse(cc.ReplaceAllWithStars(ref text));
            Assert.IsTrue(text.Contains("4111-111111111111"));
        }

        [TestMethod]
        public void single_quote_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with single quote '4111-1111-1111-1111' and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void double_quote_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with braces \"4111-1111-1111-1111\" and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void encoded_text_around_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with encoded text &quote;4111-1111-1111-1111&quote; and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void encoded_text_before_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with encoded text &quote;4111-1111-1111-1111 and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void encoded_text_after_credit_card_number()
        {
            CreditCard cc = new CreditCard();

            string text = "credit card with encoded text 4111-1111-1111-1111&quote; and ...";
            Assert.IsTrue(cc.ReplaceAllWithStars(ref text));
            Assert.IsFalse(text.Contains("4111-1111-1111-1111"));
        }

        [TestMethod]
        public void trim_non_digits()
        {
            CreditCardDerived ccd = new CreditCardDerived();
            string text = "tga12341234ytm";
            string expected = "12341234";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void trim_non_digits_all_digits()
        {
            CreditCardDerived ccd = new CreditCardDerived();
            string text = "12341234";
            string expected = "12341234";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void trim_non_digits_no_digits()
        {
            CreditCardDerived ccd = new CreditCardDerived();
            string text = "ytm";
            string expected = "";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void trim_non_digits_single_char()
        {
            CreditCardDerived ccd = new CreditCardDerived();
            string text = "y";
            string expected = "";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void trim_non_digits_zero_length()
        {
            CreditCardDerived ccd = new CreditCardDerived();
            string text = "";
            string expected = "";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void trim_non_digits_big_text()
        {
            // This test was written when the function was refactored for performance. This test
            // should take no more than 1 second to run. Previously it was taking 21 seconds to run.
            int repeat = 10000;
            CreditCardDerived ccd = new CreditCardDerived();
            string lotsOfCharacters = String.Join("", Enumerable.Repeat("abcdef", repeat).ToArray());
            string text = lotsOfCharacters + "1" + lotsOfCharacters;
            string expected = "1";
            string actual = ccd.TrimNotDigits_ForUnitTest(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void pass_null_as_string_to_function()
        {
            CreditCardDerived ccd = new CreditCardDerived();

            string text = null;
            bool result = ccd.ReplaceAllButLastFourWithStars(ref text);
            Assert.IsFalse(result);
            Assert.IsNull(text);

            text = String.Empty;
            result = ccd.ReplaceAllButLastFourWithStars(ref text);
            Assert.IsFalse(result);
            Assert.AreEqual(String.Empty, text);


        }
    }
}
