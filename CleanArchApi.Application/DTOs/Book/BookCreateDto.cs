namespace CleanArchApi.Application.DTOs.Book;

public class BookCreateDto : IBookDto
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public int PublisherId { get; set; }
}
