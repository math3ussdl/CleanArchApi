namespace CleanArchApi.Application.DTOs.Author;

public class AuthorCreateDto : IAuthorDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
}
