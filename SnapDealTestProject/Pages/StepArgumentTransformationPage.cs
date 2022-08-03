using System;

namespace SnapDealTestProject.Pages
{
    using TechTalk.SpecFlow;

    [Binding]
    public class StepArgumentTransformationPage
    {
        [StepArgumentTransformation(@"(?:(\d*) day(?:s)?(?:, )?)?(?:(\d*) hour(?:s)?(?:, )?)?(?:(\d*) minute(?:s)?(?:, )?)?(?:(\d*) second(?:s)?(?:, )?)?")]
        public TimeSpan convertToTimeSpan(String days, String hours, String minutes, String seconds)
        {
            int daysValue;
            int hoursValue;
            int minutesValue;
            int secondsValue;

            int.TryParse(days, out daysValue);
            int.TryParse(hours, out hoursValue);
            int.TryParse(minutes, out minutesValue);
            int.TryParse(seconds, out secondsValue);

            return new TimeSpan(daysValue, hoursValue, minutesValue, secondsValue);
        }
    }
}
