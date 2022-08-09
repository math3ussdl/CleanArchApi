namespace CleanArchApi.Application.Contracts.Infrastructure;

using Models;

public interface IEmailSender
{
	Task<bool> SendEmail(Email email);
}
