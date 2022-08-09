namespace CleanArchApi.Application;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ApplicationServicesRegistration
{
	public static IServiceCollection ConfigureApplicationServices(
		this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());

		return services;
	}
}
