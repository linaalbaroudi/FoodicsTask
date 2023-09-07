using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
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
        // declarations
        // public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public IWebDriver driver;
        public Actions actions;
        public IJavaScriptExecutor jsExecutor;
        public String browserName;
        /*public ExtentReports extent;
        public ExtentTest test;*/

        public IWebDriver GetDriver()
        {
            return driver;
        }

        public void initBrowser(String browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;
            }
        }

        [SetUp]
        public void setup()
        {
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitTimeOut);

            // setting up actions for scroll down action
            actions = new Actions(driver);

            // setting up Java Script Executor
            jsExecutor = (IJavaScriptExecutor)driver;

            // opening the base url and setting up the browser's window
            driver.Manage().Window.Maximize();
            driver.Url = baseURL;

        }

        /*//report file
        [OneTimeSetUp]
        public void ReportSetup()
        {
            String workingDir = Environment.CurrentDirectory;
            String projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
            var htmlReporter = new ExtentHtmlReporter(projectDir + "//index.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("enviornment", "QA");
            extent.AddSystemInfo("host", "localhost");
            extent.AddSystemInfo("reporter", "lina albaroudi");
        }*/

        [TearDown]
        public void TearDown()
        {
            // Step7: Close the browser window
            driver.Close();
        }
    }
}
