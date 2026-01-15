using Reqnroll;

namespace ReqOverflow.Specs.WebUI.Support;

[Binding]
public class BrowserHooks(BrowserContext browserContext, TestFolders testFolders, ScenarioContext scenarioContext)
{
    [AfterScenario("@web", Order = 1)]
    public void HandleWebErrors()
    {
        if (scenarioContext.TestError != null && browserContext.IsDriverCreated)
        {
            var fileName = testFolders.GetScenarioSpecificFileName();
            browserContext.TakeScreenshot(testFolders.OutputFolder, fileName);
        }
    }
}