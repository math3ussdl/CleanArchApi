namespace CleanArchApi.Domain.Common;

public abstract class BaseDomainEntity
{
	public int Id { get; set; }
	public DateTime CreateAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime UpdatedAt { get; set; }
	public string UpdatedBy { get; set; }
}
