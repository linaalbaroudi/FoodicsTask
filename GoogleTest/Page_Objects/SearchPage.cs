using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace GoogleTest.Page_Objects
{
    internal class SearchPage
    {
        [FindsBy(How = How.Id, Using = "APjFqb")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#rso a > h3")]
        public IList<IWebElement> SearchResults { get; set; }

        // sometimes this step fails because the next button is not always displayed. Please rerun the test if the test fails here
        [FindsBy(How = How.Id, Using = "pnnext")]
        public IWebElement NextButton { get; set; }

        [FindsBy(How = How.Id, Using = "bres")]
        public IWebElement SearchSuggestions { get; set; }

        public SearchPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
