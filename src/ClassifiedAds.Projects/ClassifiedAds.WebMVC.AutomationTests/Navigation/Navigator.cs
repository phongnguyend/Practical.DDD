using ClassifiedAds.WebMVC.AutomationTests.Pages;
using OpenQA.Selenium;

namespace ClassifiedAds.WebMVC.AutomationTests.Navigation
{
    public class Navigator
    {
        IWebDriver _driver;
        IJavaScriptExecutor _javaScriptExecutor;

        public Navigator(IWebDriver driver)
        {
            _driver = driver;
            _javaScriptExecutor = (IJavaScriptExecutor)_driver;
        }

        public void HomePage()
        {
            var logoutLink = _driver.FindElement(By.CssSelector("body > header > nav > div > div > ul > li:nth-child(1) > a"));
            _javaScriptExecutor.ExecuteScript("arguments[0].click();", logoutLink);
        }

        public void PrivacyPage()
        {
            var logoutLink = _driver.FindElement(By.CssSelector("body > header > nav > div > div > ul > li:nth-child(2) > a"));
            _javaScriptExecutor.ExecuteScript("arguments[0].click();", logoutLink);
        }

        public LoginPage LoginPage()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.GoTo();
            return loginPage;
        }

        public void Logout()
        {
            var logoutLink = _driver.FindElement(By.CssSelector("body > header > nav > div > div > ul > li:nth-child(4) > a"));
            _javaScriptExecutor.ExecuteScript("arguments[0].click();", logoutLink);
        }
    }
}
