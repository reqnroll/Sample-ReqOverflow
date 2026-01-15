using System;
using OpenQA.Selenium;
using ReqOverflow.Specs.Support;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Drivers;

public class LoginPageDriver(BrowserContext browserContext) : ActionAttempt<LoginInputModel, string>
{
    private const string PageUrl = "/Login";
    private IWebElement Name => browserContext.Driver.FindElement(By.Id("Name"));
    private IWebElement Password => browserContext.Driver.FindElement(By.Id("Password"));
    private IWebElement LoginButton => browserContext.Driver.FindElement(By.Id("LoginButton"));

    public event Action<LoginInputModel, string> OnAuthenticated;

    public void GoTo()
    {
        browserContext.NavigateTo(PageUrl);
    }

    protected override string DoAction(LoginInputModel loginInput)
    {
        GoTo();
        Name.SendKeys(loginInput.Name);
        Password.SendKeys(loginInput.Password);
        browserContext.SubmitFormWith(LoginButton, true);
        browserContext.AssertNotOnPath(PageUrl);
        OnAuthenticated?.Invoke(loginInput, loginInput.Name);
        return loginInput.Name;
    }
}