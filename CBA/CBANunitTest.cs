using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CBA
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
    class CBANunitTest
    {
        IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void Initialize()
        {
            driver.Navigate().GoToUrl("https://www.commbank.com.au");
        }
        [Test]
        public void ExecuteTest()
        {
            // Unable to find Travel money overseas section, thus using Travel product instead
            driver.FindElement(By.XPath("//div[contains(text(),'Travel product')]"), 10).Click();

            // Find subtopic
            Assert.IsTrue(driver.FindElement(By.ClassName("card-section"), 5).Displayed);

            // Click Netbank login
            driver.FindElement(By.ClassName("login"), 5).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//a[contains(text(),'NetBank log on')]"), 5).Click();

            // 
            Assert.IsTrue(driver.FindElement(By.Name("txtMyClientNumber$field")).Displayed);
            Assert.IsTrue(driver.FindElement(By.Name("txtMyPassword$field")).Displayed);
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
