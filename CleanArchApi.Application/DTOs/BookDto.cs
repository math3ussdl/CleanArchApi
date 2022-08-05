namespace CleanArchApi.Application.DTOs;

using Common;

public class BookDto : BaseDto
{
	public string Title { get; set; }
	public bool IsDisponible { get; set; }
	public AuthorDto Author { get; set; }
	public PublisherDto Publisher { get; set; }
}
