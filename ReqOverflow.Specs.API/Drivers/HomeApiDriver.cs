using System;
using ReqOverflow.Specs.API.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.API.Drivers
{
    public class HomeApiDriver
    {
        private readonly WebApiContext _webApiContext;

        public HomeApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public HomePageModel GetHomePageModel()
        {
            return _webApiContext.ExecuteGet<HomePageModel>("/api/home");
        }
    }
}
