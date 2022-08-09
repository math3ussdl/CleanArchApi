namespace CleanArchApi.Application.DTOs.Publisher;

using Common;

public class PublisherUpdateDto : BaseDto, IPublisherDto
{
	public string Name { get; set; }
}
