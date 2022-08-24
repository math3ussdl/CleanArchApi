namespace CleanArchApi.Application.Responses;

public class BaseResponse
{
	public bool Success { get; set; }
	public string Message { get; set; }
	public List<string> Errors { get; set; }
	public ErrorTypes ErrorType { get; set; }
}
