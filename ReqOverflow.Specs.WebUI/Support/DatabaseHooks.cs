// This file is a copy of ReqOverflow.Specs.Controller/Support/DatabaseHooks.cs.
// It has been copied for the sake of the demonstration. So that the different 
// automation targets can be studied independently.

using ReqOverflow.Web.DataAccess;
using Reqnroll;

// ReSharper disable once CheckNamespace
namespace ReqOverflow.Specs.Support;

[Binding]
public class DatabaseHooks
{
    private readonly DataContext.IDataPersist _dataPersist = new DataContext.InMemoryPersist();

    [BeforeScenario(Order = 100)]
    public void ResetDatabaseToBaseline()
    {
        // configure app to use in-memory database
        DataContext.CreateDataPersist = () => _dataPersist;
            
        ClearDatabase();
        DomainDefaults.AddDefaultUsers();
    }

    private static void ClearDatabase()
    {
        var db = new DataContext();
        db.TruncateTables();
    }
}