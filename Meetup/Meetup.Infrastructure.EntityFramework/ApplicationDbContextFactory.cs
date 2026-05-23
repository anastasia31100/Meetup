using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Meetup.Infrastructure.EntityFramework;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Настройка подключения к PostgreSQL
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=MeetupDb;Username=postgres;Password=123");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
