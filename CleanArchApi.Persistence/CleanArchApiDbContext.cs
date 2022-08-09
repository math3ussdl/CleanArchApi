namespace CleanArchApi.Persistence;

using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public class CleanArchApiDbContext : DbContext
{
	public CleanArchApiDbContext(DbContextOptions<CleanArchApiDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CleanArchApiDbContext).Assembly);
	}

	public override Task<int> SaveChangesAsync(
		CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
		{
			entry.Entity.UpdatedAt = DateTime.Now;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreateAt = DateTime.Now;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}

	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<Publisher> Publishers { get; set; }
}
