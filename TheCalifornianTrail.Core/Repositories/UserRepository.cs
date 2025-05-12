using Microsoft.EntityFrameworkCore;
using TheCalifornianTrail.Core.Entities;

namespace TheCalifornianTrail.Core.Repositories;

public class UserRepository
{
    private readonly TrailDbContext _context;

    public UserRepository(TrailDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateUser(string name, string email)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == email);
        if (exists)
            return Result.Failure("Email already exists");
        await Task.Delay(50);
        _context.Users.Add(new User { Name = name, Email = email });
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
}

public record Result(bool Success, string? Error)
{
    public static Result Failure(string msg) => new(false, msg);
    public static Result Ok() => new(true, null);
}
