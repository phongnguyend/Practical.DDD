using OpenQA.Selenium;

namespace ClassifiedAds.WebMVC.AutomationTests.Pages
{
    public class ConsentPage
    {
        IWebDriver _driver;

        public ConsentPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool RememberMyDecision
        {
            set
            {
                var remember = _driver.FindElement(By.Id("RememberConsent"));
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

        public void Confirm()
        {
            var loginButton = _driver.FindElement(By.CssSelector("button.btn.btn-primary"));
            loginButton.Click();
        }
    }
}
