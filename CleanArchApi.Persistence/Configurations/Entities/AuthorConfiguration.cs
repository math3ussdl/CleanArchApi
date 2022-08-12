namespace CleanArchApi.Persistence.Configurations.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.HasData(
			new Author
			{
				Id = 1,
				Name = "ADMIN",
				Email = "limabrot879@gmail.com",
				Phone = "+55 (81) 93618-4134"
			}
		);
	}
}
