namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class DeletePublisherCommandHandler :
  IRequestHandler<DeletePublisherCommand, BaseResponse>
{
  private readonly IPublisherRepository _publisherRepository;
  private readonly IMapper _mapper;

  public DeletePublisherCommandHandler(IPublisherRepository publisherRepository,
    IMapper mapper)
  {
    _publisherRepository = publisherRepository;
    _mapper = mapper;
  }

  public async Task<BaseResponse> Handle(DeletePublisherCommand request,
    CancellationToken cancellationToken)
  {
    BaseResponse response = new();

    try
    {
      var publisher = await _publisherRepository.Get(request.Id);

      if (publisher == null)
      {
        response.Success = false;
        response.ErrorType = ErrorTypes.NotFound;
        response.Message = "Publisher not found!";
      }
      else
      {
        await _publisherRepository.Delete(publisher);

        response.Success = true;
        response.Message = "Publisher successfully deleted!";
      }
    }
    catch (System.Exception ex)
    {
      response.Success = false;
      response.Message = ex.Message;
      response.ErrorType = ErrorTypes.Internal;
    }

    return response;
  }
}
