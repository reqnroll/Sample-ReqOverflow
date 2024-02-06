using System;
using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;
using Reqnroll;
using Reqnroll.Assist;

namespace ReqOverflow.Specs.Controller.Support
{
    [Binding]
    public class Conversions
    {
        [StepArgumentTransformation]
        public AskInputModel ConvertAskInputModel(Table questionTable)
        {
            return questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
        }
    }
}
