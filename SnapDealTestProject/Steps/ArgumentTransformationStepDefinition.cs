using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Steps
{
    using TechTalk.SpecFlow;

    [Binding]
    public class ArgumentTransformationStepDefinition
    {
        [Given(@"I open timestamp converter")]
        public void GivenIOpenTimestampToMinuteConverter()
        {
            Console.WriteLine("Opened TimeStamp to Minute Converter");
        }

        [Then(@"I convert (.*) into timestamp")]
        public void ThenIConvertIntotimestamp(TimeSpan convertTimeSpan)
        {
            Console.WriteLine("Converted TimeSpan: {0}", convertTimeSpan);
        }
    }
}
