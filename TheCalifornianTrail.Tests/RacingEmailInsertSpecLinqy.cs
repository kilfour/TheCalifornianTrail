using TheCalifornianTrail.Core.Repositories;
using TheCalifornianTrail.Tests.TestFixtures;
using QuickAcid;
using QuickAcid.Bolts.Nuts.ASync;
using QuickMGenerate;

namespace TheCalifornianTrail.Tests;

public class RacingEmailInsertSpecLinqy
{
    [Fact]
    public async Task GOGOGO()
    {
        var dbName = Guid.NewGuid().ToString();
        var run =
            //from dbName in "db-name".AlwaysReported(() => Guid.NewGuid().ToString())
            from result in "send email".Act(async () =>
            {
                var db = DbContextFactory.CreateInMemory(dbName);
                var email = "myemail@provider.com";
                var repo = new UserRepository(db);
                var result = await repo.CreateUser("Boom", email);
            })
            from spec in "email should be unique".Assay(() =>
                {
                    var db = DbContextFactory.CreateInMemory(dbName);
                    Console.WriteLine(db.Users.Count());
                    var duplicates = db.Users.GroupBy(u => u.Email).Any(g => g.Count() > 1);
                    return Task.FromResult(true);
                })
            select Acid.Test;

        var report = await run.ReportIfFailedAsync(10, 100);
        if (report != null)
            Assert.Fail(report.ToString());
    }
}