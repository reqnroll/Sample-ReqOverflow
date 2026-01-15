using ReqOverflow.Specs.Support;
using ReqOverflow.Web.Models;
using Reqnroll;

namespace ReqOverflow.Specs.Controller.Support;

[Binding]
public class Conversions
{
    [StepArgumentTransformation]
    public AskInputModel ConvertAskInputModel(Table questionTable)
    {
        return questionTable.CreateInstance(DomainDefaults.GetDefaultAskInput);
    }
}