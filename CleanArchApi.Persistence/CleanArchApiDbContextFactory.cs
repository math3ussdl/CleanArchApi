namespace CleanArchApi.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class CleanArchApiDbContextFactory :
	IDesignTimeDbContextFactory<CleanArchApiDbContext>
{
	public CleanArchApiDbContext CreateDbContext(string[] args)
	{
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		var builder = new DbContextOptionsBuilder<CleanArchApiDbContext>();
		var connectionString = configuration.GetConnectionString
			("CleanArchApiDbConnection");

		builder.UseSqlServer(connectionString);

		return new CleanArchApiDbContext(builder.Options);
	}
}
