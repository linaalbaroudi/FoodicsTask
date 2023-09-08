using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace GoogleTest.Page_Objects
{
    internal class LandingPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "APjFqb")]
        public IWebElement SearchBox { get; set; }

        public LandingPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // this method could be ovveriden from inherit parent class
        public SearchPage search(string keyword)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(keyword);
            SearchBox.SendKeys(Keys.Enter);
            return new SearchPage(driver);
        }
    }
}
