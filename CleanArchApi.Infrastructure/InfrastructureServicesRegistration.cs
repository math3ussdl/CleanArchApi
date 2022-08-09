namespace CleanArchApi.Infrastructure;

using Application.Models;
using Application.Contracts.Infrastructure;
using Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServicesRegistration
{
	public static IServiceCollection ConfigureInfrastructureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
		services.AddTransient<IEmailSender, EmailSender>();

		return services;
	}
}
