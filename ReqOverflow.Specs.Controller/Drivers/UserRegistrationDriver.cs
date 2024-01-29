using System;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Controllers;
using ReqOverflow.Web.Models;

namespace ReqOverflow.Specs.Controller.Drivers
{
    public class UserRegistrationDriver : ActionAttempt<RegisterInputModel, UserReferenceModel>
    {
        public UserRegistrationDriver() : base(registerInput =>
        {
            var controller = new UserController();
            return controller.Register(registerInput);
        })
        {
        }
    }
}
