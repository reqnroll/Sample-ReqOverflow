using ReqOverflow.Specs.API.Support;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.API.Drivers;

public class HomeApiDriver(WebApiContext webApiContext)
{
    public HomePageModel GetHomePageModel()
    {
        return webApiContext.ExecuteGet<HomePageModel>("/api/home");
    }
}