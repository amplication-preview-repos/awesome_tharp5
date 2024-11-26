using Claimly.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Claimly.Infrastructure;

public class ClaimlyDbContext : DbContext
{
    public ClaimlyDbContext(DbContextOptions<ClaimlyDbContext> options)
        : base(options) { }

    public DbSet<ClaimDbModel> Claims { get; set; }

    public DbSet<ClaimStatusDbModel> ClaimStatuses { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<PolicyDbModel> Policies { get; set; }
}
