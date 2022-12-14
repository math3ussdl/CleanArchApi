namespace CleanArchApi.Domain;

using Common;

public class Author : BaseDomainEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
}
