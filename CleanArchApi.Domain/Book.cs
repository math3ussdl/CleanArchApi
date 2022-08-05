namespace CleanArchApi.Domain;

using Common;

public class Book : BaseDomainEntity
{
	public string Title { get; set; }
	public bool IsDisponible { get; set; }
	public Author Author { get; set; }
	public Publisher Publisher { get; set; }
}
