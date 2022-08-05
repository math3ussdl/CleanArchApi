namespace CleanArchApi.Application.DTOs.Book;

using Author;
using Common;
using Publisher;

public class BookDetailDto : BaseDto
{
	public string Title { get; set; }
	public bool IsDisponible { get; set; }
	public AuthorDetailDto Author { get; set; }
	public PublisherDetailDto Publisher { get; set; }
	public DateTime CreateAt { get; set; }
	public string CreatedBy { get; set; }
}
