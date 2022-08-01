namespace SnapDealTestProject.Library.Extensions
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;

    using SnapDealTestProject.Interfaces;

    /// <summary>
    /// DriverExtensions class
    /// </summary>
    public static partial class DriverExtensions
    {
        /// <summary>
        /// Web Driver
        /// </summary>
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        /// <summary>
        /// Initialize WebDriver
        /// </summary>
        public static void InitializeWebDriver()
        {
            var executionType = ConfigurationManager.AppSettings["ExecutionType"];
            var browser = ConfigurationManager.AppSettings["Browser"];

            if (executionType == "Grid")
            {
                driver.Value = new RemoteWebDriver(new Uri("http://10.178.169.23:4444/"), getChromeOptions());
            }
            else
            {
                switch (browser)
                {
                    case "Chrome":
                        InitializeChromeDriver();
                        break;
                    case "Firefox":
                        InitializeFirefoxDriver();
                        break;
                } 
            }
        }

        /// <summary>
        /// Creates an object for the given page type
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <typeparam name="T"> The type of the object </typeparam>
        /// <returns> The instance </returns>
        public static T CreatePageInstance<T>(params object[] args) where T : ICreatablePageObject
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// Get Project Location
        /// </summary>
        /// <returns>Returns project location</returns>
        public static string GetProjectLocation()
        {
            var project = Assembly.GetCallingAssembly().GetName().Name;
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            directory = directory.Substring(0, directory.LastIndexOf(project));
            directory = Path.Combine(directory, project);
            Console.WriteLine("path is: " + directory);
            return directory;
        }

        /// <summary>
        /// Initialize Chrome WebDriver
        /// </summary>
        private static void InitializeChromeDriver()
        {
            driver.Value = new ChromeDriver(GetProjectLocation() + @"\\Drivers", getChromeOptions());
        }

        private static ChromeOptions getChromeOptions()
        {
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments(
                "--start-maximized",
                "--test-type",
                "--disable-gpu",
                "--disable-backgrounding-occluded-windows",
                "--disable-infobars"); // disable "Chrome is being controlled by automated test software" InfoBar

            // disable "Do you want to save password for this site" dialog
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            return chromeOptions;
        }

        /// <summary>
        /// The get Firefox options.
        /// </summary>
        /// <param name="pathToBrowserExecutable">The path to the browser executable file.</param>
        /// <returns>
        /// The <see cref="FirefoxOptions"/> for released and unreleased versions of Firefox.
        /// </returns>
        private static void InitializeFirefoxDriver()
        {
            /*var browserOptions = new FirefoxOptions();
            var profile = new FirefoxProfile();

            //profile.SetPreference("javascript.enabled", true);
            browserOptions.Profile = profile;
            browserOptions.BrowserExecutableLocation = GetProjectLocation() + @"\\Drivers";*/

            FirefoxDriverService firefoxDriverService =
                FirefoxDriverService.CreateDefaultService(GetProjectLocation() + @"\\Drivers");
            driver.Value = new FirefoxDriver(firefoxDriverService);

            driver.Value.Manage().Window.Maximize();
        }

        public static Screenshot GetScreenshot(String path)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver.Value).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return screenshot;
        }
    }
}
