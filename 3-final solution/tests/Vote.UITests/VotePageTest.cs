using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Vote.UITests.Pages;
using OpenQA.Selenium.PhantomJS;
using System.Configuration;
using OpenQA.Selenium.Remote;

namespace Vote.UITests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class VotePageTest
    {
        private IWebDriver Driver { get; set; }
        private WebDriverWait Wait { get; set; }
        private string baseurl;
        public VotePageTest()
        {
        }


        private void MyTestInitialize()
        {

            baseurl = ConfigurationManager.AppSettings["baseurl"].ToString();
            if (ConfigurationManager.AppSettings["usePhantom"].ToString() == "true")
            {
                if (ConfigurationManager.AppSettings["useDocker"].ToString() == "true")
                {
                    this.Driver = new RemoteWebDriver(new Uri(ConfigurationManager.AppSettings["webDriverURL"].ToString()), DesiredCapabilities.Chrome());
                    baseurl = ConfigurationManager.AppSettings["baseurlDocker"].ToString();

                }
                else
                {
                    this.Driver = new PhantomJSDriver();
                    var driverService = PhantomJSDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;

                    this.Driver = new PhantomJSDriver(driverService);
                }

            }
            else
            {
                this.Driver = new ChromeDriver();

            }

            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(30));
        }

        private void MyTestCleanup()
        {

            this.Driver.Quit();
        }

        public void VoteForOptionA()
        {
            MyTestInitialize();
            VotePage p = new VotePage(Driver);
            p.Navigate(baseurl);
            p.VoteA.Click();
            MyTestCleanup();
        }


        public void VoteForOptionB()
        {
            MyTestInitialize();
            VotePage p = new VotePage(Driver);
            p.Navigate(baseurl);
            p.VoteB.Click();
            MyTestCleanup();
        }

        [TestMethod]
        public void VoteSequence()
        {
            for (int i = 0; i < 30; i++)
            {
                VoteForOptionA();

            }
            for (int i = 0; i < 30; i++)
            {
                VoteForOptionB();

            }
        }

    }
}
