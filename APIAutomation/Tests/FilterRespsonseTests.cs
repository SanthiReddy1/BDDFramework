using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Tests
{
    using System.Collections;

    using Newtonsoft.Json.Linq;

    using NUnit.Framework;

    public class FilterRespsonseTests
    {

        [Test]
        [Category("API")]
        public void performOperationsOnComplexJson()
        {
            JObject jObject = JObject.Parse(this.Response());

            // Get Purchase Amount
            int purchaseAmount = (int)jObject.SelectToken("$.dashboard.purchaseAmount");
            Console.WriteLine("Purchase Amount: {0}", purchaseAmount);

            // Get total number of couses
            IEnumerable<JToken> jTokens = jObject.SelectTokens("$.courses[*]");
            Console.WriteLine("Number of couses: {0}", jTokens.Count());

            // Get first course title
            Console.WriteLine("Title of first course: {0}", jObject.SelectToken("$.courses[0].title"));

            // Get copies and price of each course to calculate total price
            int totalPrice = jTokens.Sum(x => (int)x.SelectToken("price") * (int)x.SelectToken("copies"));
            Assert.AreEqual(purchaseAmount, totalPrice, purchaseAmount + " Purchase Amount is not equal to Total Price " + totalPrice);

            for (int i=0; i < jTokens.Count(); i++)
            {
                Console.WriteLine(jObject.SelectToken("$.courses["+ i +"]"));
            }

            //Print copies sold by certain course
            Console.WriteLine("RPA copies {0}: " + jObject.SelectToken("$.courses[?(@.title == 'RPA')].copies"));
        }

        private string Response()
        {
            return "{\r\n" +
            "\"dashboard\": {\r\n" +
                "\"purchaseAmount\": 910,\r\n" +
                "\"website\": \"rahulsheetyacademy.com\"\r\n" +
            "},\r\n" +
            "\"courses\": [\r\n" +
            "{\r\n" +
                "\"title\": \"Selenium Python\",\r\n" +
                "\"price\": 50,\r\n" +
                "\"copies\": 6\r\n" +
           "},\r\n" +
           "{\r\n" +
                "\"title\": \"Cypress\",\r\n" +
                "\"price\": 40,\r\n" +
                "\"copies\": 4\r\n" +
            "},\r\n" +
            "{\r\n" +
                "\"title\": \"RPA\",\r\n" +
                "\"price\": 45,\r\n" +
                "\"copies\": 10\r\n" +
           "}\r\n" +
            "]\r\n" +
            "}\r\n";
        }
    }
}
