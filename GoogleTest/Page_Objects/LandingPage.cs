using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace GoogleTest.Page_Objects
{
    internal class LandingPage
    {
        [FindsBy(How = How.Id, Using = "APjFqb")]
        public IWebElement SearchBox { get; set; }

        public LandingPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
