namespace CleanArchApi.Persistence;

using Application.Persistence.Contracts;
using Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PersistenceServicesRegistration
{
	public static IServiceCollection ConfigurePersistenceServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<CleanArchApiDbContext>(opts =>
		{
			opts.UseSqlServer(
				configuration.GetConnectionString("CleanArchApiDbConnection"));
		});

		services.AddScoped(
			typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

		services.AddScoped<IAuthorRepository, AuthorRepository>();
		services.AddScoped<IBookRepository, BookRepository>();
		services.AddScoped<IPublisherRepository, PublisherRepository>();

		return services;
	}
}
