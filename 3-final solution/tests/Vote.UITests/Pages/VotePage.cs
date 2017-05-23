using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;

namespace Vote.UITests.Pages
{
    public class VotePage
    {
        private IWebDriver _browser = null;

        [FindsBy(How = How.Id, Using = "a")]
        public IWebElement VoteA { get; set; }
        [FindsBy(How = How.Id, Using = "b")]
        public IWebElement VoteB { get; set; }

        public VotePage(IWebDriver driver)
        {
            _browser = driver;
            PageFactory.InitElements(_browser, this);
        }

        public void Navigate(string url)
        {
            _browser.Navigate().GoToUrl(url);
        }
    }
}
