using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace GoogleTest.Page_Objects
{
    internal class SearchPage 
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "APjFqb")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#rso a > h3")]
        public IList<IWebElement> SearchResults { get; set; }

        // sometimes this step fails. Please rerun the test if the test fails here.
        [FindsBy(How = How.Id, Using = "pnnext")]
        public IWebElement NextButton { get; set; }

        [FindsBy(How = How.Id, Using = "bres")]
        public IWebElement SearchSuggestions { get; set; }

        public SearchPage(IWebDriver driver)
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
            // Step3: Assert that number of results exist on UI
            Assert.IsNotEmpty(SearchResults);
            return new SearchPage(driver);
        }

        public SearchPage nextPage(IJavaScriptExecutor jsExecutor)
        {
            jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            NextButton.Click();
            return new SearchPage(driver);
        }

        public void printResults()
        {
            TestContext.WriteLine($"\nNumber of search results in Page2 = {SearchResults.Count}\n");

            foreach (IWebElement searchResult in SearchResults)
            {
                TestContext.WriteLine(searchResult.Text);
            }
        }

        public bool isSuggestionDisplayed(IJavaScriptExecutor jsExecutor)
        {
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", SearchSuggestions);
            bool isDisplayed = SearchSuggestions.Displayed;
            Assert.IsTrue(isDisplayed);
            return isDisplayed;
        }
    }
}
