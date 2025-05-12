using TheCalifornianTrail.Core.Repositories;
using TheCalifornianTrail.Tests.TestFixtures;
using QuickAcid;
using QuickMGenerate;

namespace TheCalifornianTrail.Tests;

public class RacingEmailInsertSpec
{
    [Fact(Skip = "WIP")]
    public void Two_users_with_same_email_should_not_both_succeed()
    {
        SystemSpecs
            .Define()
            .Fuzzed("email", MGen.Constant("d"))
            .Do("insert one user", async ctx =>
                {
                    var db = DbContextFactory.CreateInMemory(Guid.NewGuid().ToString());
                    var email = ctx.GetItAtYourOwnRisk<string>("email");
                    var repo = new UserRepository(db);
                    var result = await repo.CreateUser("Boom", email);
                    Console.WriteLine(result);
                })
            .Assay("email should be unique", ctx =>
                {
                    var db = DbContextFactory.CreateInMemory(Guid.NewGuid().ToString());
                    Console.WriteLine(db.Users.Count());
                    var duplicates = db.Users.GroupBy(u => u.Email).Any(g => g.Count() > 1);
                    return !duplicates;
                })
            .DumpItInAcid()
            .AndCheckForGold(1, 5);
    }
}