using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CSharpNUnitProject.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;

namespace GoogleTest.utilities
{
    public class Setup
    {
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public Actions? actions;
        public IJavaScriptExecutor? jsExecutor;
        public string? browserName;
        public ExtentReports? extent;
        public ExtentTest? test;

        public IWebDriver GetDriver()
        {
            return driver.Value!;
        }

        public void initBrowser(String browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
            }
        }

        [OneTimeSetUp]
        public void ReportSetup()
        {
            String workingDir = Environment.CurrentDirectory;
            String projectDir = Directory.GetParent(workingDir)!.Parent!.Parent!.FullName;
            var htmlReporter = new ExtentSparkReporter(projectDir + "//index.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("enviornment", "QA");
            extent.AddSystemInfo("host", "localhost");
            extent.AddSystemInfo("reporter", "lina albaroudi");
        }

        [SetUp]
        public void setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            // getting configuration data
            browserName = TestContext.Parameters["browserName"]!;
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"]!;
            }
            int implicitTimeOut = int.Parse(ConfigurationManager.AppSettings["implicitWait"]!, NumberStyles.AllowThousands, new CultureInfo("en-au"));
            String baseURL = ConfigurationManager.AppSettings["url"]!;

            // setting up web drivers
            initBrowser(browserName);

            // setting up implicit wait 
            driver.Value!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitTimeOut);

            // setting up actions for scroll down action
            actions = new Actions(driver.Value);

            // setting up Java Script Executor
            jsExecutor = (IJavaScriptExecutor)driver.Value;

            // opening the base url and setting up the browser's window
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = baseURL;

        }

        public static JsonReader GetJR()
        {
            return new JsonReader();
        }

        [TearDown]
        public void CloseBrowser()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            DateTime time = DateTime.Now;
            String fileName = "screenshot_" + time.ToString("h-mm-ss") + ".png";
            if (testStatus == TestStatus.Failed)
            {
                var logd = TestContext.CurrentContext.Result.StackTrace;
                test.Fail("Test Failed!", TakeScreenShot(driver.Value!, fileName).Build());
                test.Log(Status.Fail, "error logs " + logd);
            }
            else if (testStatus == TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value.Close();
        }

        public MediaEntityBuilder TakeScreenShot(IWebDriver d, String name)
        {
            ITakesScreenshot ss = (ITakesScreenshot)d;
            var screenshot = ss.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name);
        }
    }
}
