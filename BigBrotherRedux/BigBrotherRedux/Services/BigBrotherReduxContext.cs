using BigBrotherRedux.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigBrotherRedux.Services;

public class BigBrotherReduxContext : DbContext
{
    public BigBrotherReduxContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<UserIPData> UserIPData { get; set; }

}

