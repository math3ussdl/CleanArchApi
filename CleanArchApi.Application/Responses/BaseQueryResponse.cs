namespace CleanArchApi.Application.Responses;

public class BaseQueryResponse<T> : BaseResponse
{
  public T Data { get; set; }
}
