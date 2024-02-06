using System;
using Reqnroll;

namespace ReqOverflow.Specs.WebUI.Support
{
    [Binding]
    public class AppHostingHooks
    {
        [AfterTestRun]
        public static void StopApp()
        {
            AppHostingContext.StopApp();
        }
    }
}
