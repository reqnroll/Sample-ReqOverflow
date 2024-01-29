using System;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers
{
    public class LoginDriver : ActionAttempt<LoginInputModel, string>
    {
        public event Action<LoginInputModel, string> OnAuthenticated;
        
        protected override string DoAction(LoginInputModel loginInput)
        {
            var controller = new AuthController();
            var authToken = controller.Login(new LoginInputModel { Name = loginInput.Name, Password = loginInput.Password });
            OnAuthenticated?.Invoke(loginInput, authToken);
            return authToken;
        }
    }
}
