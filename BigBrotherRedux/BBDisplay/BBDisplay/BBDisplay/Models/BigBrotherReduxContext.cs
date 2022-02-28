using Microsoft.EntityFrameworkCore;

namespace BBDisplay.Models;

public class BigBrotherReduxContext : DbContext
{
    public BigBrotherReduxContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<UserIPData> UserIPData { get; set; }
    public DbSet<UserInteraction> UserInteraction { get; set; }
    public DbSet<PageReference> PageReference { get; set; }
    public DbSet<Session> Session { get; set; }

}