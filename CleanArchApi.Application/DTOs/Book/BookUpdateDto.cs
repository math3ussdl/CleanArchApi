namespace CleanArchApi.Application.DTOs.Book;

using Common;

public class BookUpdateDto : BaseDto, IBookDto
{
	public string Title { get; set; }
	public int AuthorId { get; set; }
	public int PublisherId { get; set; }
}
