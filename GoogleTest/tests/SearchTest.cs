using GoogleTest.Page_Objects;
using GoogleTest.utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace GoogleTest.tests
{
    internal class SearchTest : Setup
    {
        private const String keyWord1 = "Banana"; //TODO: get from test data
        private const String keyWord2 = "Apple";

        [Test]
        public void searchTest()
        {
            // Step1: Open website and search for any keyword
            LandingPage landingPage = new LandingPage(GetDriver());
            landingPage.SearchBox.SendKeys(keyWord1);
            landingPage.SearchBox.SendKeys(Keys.Enter);

            // Step2: Remove the keyword and search for a new one
            SearchPage searchPage = new SearchPage(GetDriver());
            searchPage.SearchBox.Clear();
            searchPage.SearchBox.SendKeys(keyWord2);
            searchPage.SearchBox.SendKeys(Keys.Enter);

            // Step3: Assert that number of results exist on UI
            Assert.IsNotEmpty(searchPage.SearchResults);

            // Step4: Scroll down and go to the next page
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", searchPage.NextButton);
            //nextButton.Click();
            searchPage.NextButton.Click();

            // Step5: Validate if the number of results on page 2 is equal to page 3 or not

            // get search results in page 2
            Assert.IsNotEmpty(searchPage.SearchResults);

            int resultsCountPage2 = searchPage.SearchResults.Count;
            TestContext.WriteLine($"\nNumber of search results in Page2 = {resultsCountPage2}\n");

            foreach (IWebElement searchResult in searchPage.SearchResults)
            {
                TestContext.WriteLine(searchResult.Text);
            }

            // get search results in page 3
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", searchPage.NextButton);
            searchPage.NextButton.Click();

            Assert.IsNotEmpty(searchPage.SearchResults);

            int resultsCountPage3 = searchPage.SearchResults.Count;
            TestContext.WriteLine($"\nNumber of search results in Page3 = {resultsCountPage3}\n");

            foreach (IWebElement searchResult in searchPage.SearchResults)
            {
                TestContext.WriteLine(searchResult.Text);
            }

            // Validate if the number of results on page 2 is equal to page 3 or not
            string phrase = resultsCountPage2 == resultsCountPage3 ? "" : "not";
            TestContext.WriteLine($"\nsearch results in Page2 and Page3 are {phrase} equal\n");

            // Step6: Validate there are different search suggestions displayed at the end of the page
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", searchPage.SearchSuggestions);
            bool isSuggestionsDisplayed = searchPage.SearchSuggestions.Displayed;
            phrase = isSuggestionsDisplayed ? "" : "not";
            TestContext.WriteLine($"\nDifferent search suggestions are {phrase} displayed at the end of the page\n");
            Assert.IsTrue(isSuggestionsDisplayed, $"Different search suggestions are {phrase} displayed at the end of the page");

        }        
    }
}
