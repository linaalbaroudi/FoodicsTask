using GoogleTest.Page_Objects;
using GoogleTest.utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GoogleTest.tests
{
    internal class SearchTest : Setup
    {

        [Test]
        [TestCaseSource("TestDataConfig")]
        [Parallelizable(ParallelScope.All)]
        public void searchTest(String keyWord1, String keyWord2)
        {
            // Step1: Open website and search for any keyword
            LandingPage landingPage = new LandingPage(GetDriver());
            SearchPage searchPage = landingPage.search(keyWord1);

            // Step2: Remove the keyword and search for a new one
            searchPage.search(keyWord2);

            // Step4: Scroll down and go to the next page
            searchPage = searchPage.nextPage(jsExecutor!);

            // Step5: Validate if the number of results on page 2 is equal to page 3 or not
            int resultsCountPage2 = searchPage.SearchResults.Count;
            searchPage.printResults();

            searchPage = searchPage.nextPage(jsExecutor);
            int resultsCountPage3 = searchPage.SearchResults.Count;
            searchPage.printResults();

            TestContext.WriteLine($"\nSearch results in Page2 and Page3 are {printConditional(resultsCountPage2 == resultsCountPage3)}equal\n");

            // Step6: Validate there are different search suggestions displayed at the end of the page
            TestContext.WriteLine($"\nOther search suggestions are {printConditional(searchPage.isSuggestionDisplayed(jsExecutor!) )}displayed at the end of the page\n");

        }

        static String printConditional(bool condition)
        {
            return condition ? "" : "not ";
        }

        public static IEnumerable<TestCaseData> TestDataConfig()
        {
            yield return new TestCaseData(GetJR().ReadArrays("keywords1")[0], GetJR().ReadArrays("keywords1")[1]);
            yield return new TestCaseData(GetJR().ReadArrays("keywords2")[0], GetJR().ReadArrays("keywords2")[1]);
        }
    }
}
