namespace CleanArchApi.Application.DTOs.Book;

public class BookCreateDto
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public int PublisherId { get; set; }
}
