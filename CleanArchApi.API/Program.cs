using CleanArchApi.Application;
using CleanArchApi.Infrastructure;
using CleanArchApi.Persistence;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.ConfigureApplicationServices();
services.ConfigureInfrastructureServices(configuration);
services.ConfigurePersistenceServices(configuration);

services.AddControllers();

// OpenAPI/Swagger Integration
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(c =>
{
	c.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UsePathBase("/api/v1");
app.MapControllers();

app.Run();
