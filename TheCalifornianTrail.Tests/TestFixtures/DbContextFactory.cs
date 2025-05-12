using Microsoft.EntityFrameworkCore;
using TheCalifornianTrail.Core;

namespace TheCalifornianTrail.Tests.TestFixtures;

public static class DbContextFactory
{


    public static TrailDbContext CreateInMemory(string name)
    {
        var options = new DbContextOptionsBuilder<TrailDbContext>()
            .UseInMemoryDatabase(name)
            .Options;

        var context = new TrailDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}