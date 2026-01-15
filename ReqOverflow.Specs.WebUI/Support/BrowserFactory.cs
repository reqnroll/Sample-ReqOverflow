using System;
using Reqnroll.BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace ReqOverflow.Specs.WebUI.Support;

public class BrowserFactory(ScenarioContext scenarioContext)
{
    public class BrowserInstance : IDisposable
    {
        private readonly Lazy<IWebDriver> _webDriver;

        public BrowserInstance()
        {
            _webDriver = new Lazy<IWebDriver>(CreateWebDriver);
        }

        private IWebDriver CreateWebDriver()
        {
            return new ChromeDriver();
        }

        public IWebDriver GetWebDriver()
        {
            return _webDriver.Value;
        }

        public void Dispose()
        {
            if (_webDriver.IsValueCreated)
                _webDriver.Value.Dispose();
        }
    }

    // creating browser per test-thread
    private readonly IObjectContainer _browserObjectContainer = scenarioContext.ScenarioContainer.Resolve<TestThreadContext>().TestThreadContainer;
    // creating browser per feature
    //private readonly IObjectContainer _browserObjectContainer = scenarioContext.ScenarioContainer.Resolve<FeatureContext>().FeatureContainer;
    // creating browser per scenario
    //private readonly IObjectContainer _browserObjectContainer = scenarioContext.ScenarioContainer;

    private IWebDriver _browserCreated;

    public IWebDriver CreateBrowser()
    {
        _browserCreated ??= _browserObjectContainer.Resolve<BrowserInstance>().GetWebDriver();
        return _browserCreated;
    }
}