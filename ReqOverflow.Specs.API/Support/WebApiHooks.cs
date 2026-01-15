using Reqnroll;

namespace ReqOverflow.Specs.API.Support;

[Binding]
public class WebApiHooks(WebApiContext webApiContext, TestFolders testFolders, ScenarioContext scenarioContext)
{
    [AfterScenario("@webapi")]
    public void WriteLog()
    {
        if (scenarioContext.TestError != null)
        {
            var fileName = testFolders.GetScenarioSpecificFileName(".log");
            webApiContext.SaveLog(testFolders.OutputFolder, fileName);
        }
    }

    [AfterTestRun]
    public static void StopApp()
    {
        AppHostingContext.StopApp();
    }
}