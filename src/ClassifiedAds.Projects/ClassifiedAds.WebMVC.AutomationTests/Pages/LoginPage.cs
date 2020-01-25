using OpenQA.Selenium;

namespace ClassifiedAds.WebMVC.AutomationTests.Pages
{
    public class LoginPage
    {
        IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoTo()
        {
            var loginUrl = Configuration.Login_Url;
            _driver.Navigate().GoToUrl(loginUrl);
        }

        public string UserName
        {
            set
            {
                var emailInput = _driver.FindElement(By.Id("Username"));
                emailInput.SendKeys(value);
            }
        }

        public string Password
        {
            set
            {
                var emailInput = _driver.FindElement(By.Id("Password"));
                emailInput.SendKeys(value);
            }
        }

        public bool RememberMyLogin
        {
            set
            {
                var remember = _driver.FindElement(By.Id("RememberLogin"));
                if (value)
                {
                    if (!remember.Selected)
                    {
                        remember.Click();
                    }

                }
                else
                {
                    if (remember.Selected)
                    {
                        remember.Click();
                    }
                }
            }
        }

        internal ConsentPage Login()
        {
            var loginButton = _driver.FindElement(By.CssSelector("button.btn.btn-primary"));
            loginButton.Click();

            return new ConsentPage(_driver);
        }
    }
}
