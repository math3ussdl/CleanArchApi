namespace CleanArchApi.Application;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServicesRegistration
{
	public static void ConfigureApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
	}
}
