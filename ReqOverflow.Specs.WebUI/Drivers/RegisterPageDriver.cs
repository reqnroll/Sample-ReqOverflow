using OpenQA.Selenium;
using ReqOverflow.Specs.WebUI.Support;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.WebUI.Drivers;

public class RegisterPageDriver(BrowserContext browserContext) : ActionAttempt<RegisterInputModel, UserReferenceModel>
{
    private const string PageUrl = "/Register";
    private IWebElement UserName => browserContext.Driver.FindElement(By.Id("UserName"));
    private IWebElement Password => browserContext.Driver.FindElement(By.Id("Password"));
    private IWebElement PasswordReEnter => browserContext.Driver.FindElement(By.Id("PasswordReEnter"));
    private IWebElement RegisterButton => browserContext.Driver.FindElement(By.Id("RegisterButton"));

    public void GoTo()
    {
        browserContext.NavigateTo(PageUrl);
    }

    protected override UserReferenceModel DoAction(RegisterInputModel registerInput)
    {
        GoTo();
        UserName.SendKeys(registerInput.UserName);
        Password.SendKeys(registerInput.Password);
        PasswordReEnter.SendKeys(registerInput.PasswordReEnter);
        browserContext.SubmitFormWith(RegisterButton, true);
        browserContext.AssertNotOnPath(PageUrl);
        //TODO: we should parse back the registered username and ID from the success message
        return new UserReferenceModel { Name = registerInput.UserName };
    }
}