namespace CleanArchApi.Application.DTOs.Author;

using Common;

public class AuthorDetailDto : BaseDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
}
