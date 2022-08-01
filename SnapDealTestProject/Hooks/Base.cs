namespace SnapDealTestProject.Hooks
{
    using System;
    using System.IO;

    using BoDi;

    using OpenQA.Selenium;

    using SnapDealTestProject.Library.Extensions;
    using SnapDealTestProject.Utils.Assertions;

    using TechTalk.SpecFlow;

    [Binding]
    public class Base
    {
        public ScenarioContext scenarioContext;

        public string screenshotPath;

        private QualityTestCase QualityTestCase;

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext, QualityTestCase QualityTestCase)
        {
            this.scenarioContext = scenarioContext;
            this.QualityTestCase = new QualityTestCase();
            DriverExtensions.InitializeWebDriver();
        }

        [AfterScenario]
        public void TearDown()
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    string path = DriverExtensions.GetProjectLocation() + @"\Screenshots";
                    this.screenshotPath = Path.Combine(
                        path,
                        scenarioContext.ScenarioInfo.Title.Replace(" ", String.Empty) + "_"
                        + DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss") + "_ERROR" + ".png");
                    Screenshot screenshot = DriverExtensions.GetScreenshot(this.screenshotPath);
                }

                this.QualityTestCase.Cleanup(this.scenarioContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                DriverExtensions.driver.Value.Quit();
            }
        }
    }
}
