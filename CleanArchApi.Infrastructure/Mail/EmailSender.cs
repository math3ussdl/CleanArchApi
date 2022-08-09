namespace CleanArchApi.Infrastructure.Mail;

using Application.Contracts.Infrastructure;
using Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

public class EmailSender : IEmailSender
{
	private readonly EmailSettings _emailSettings;

	public EmailSender(IOptions<EmailSettings> emailSettings)
	{
		_emailSettings = emailSettings.Value;
	}

	public async Task<bool> SendEmail(Email email)
	{
		SendGridClient client = new(_emailSettings.ApiKey);
		EmailAddress to = new(email.To);
		EmailAddress from = new()
		{
			Email = _emailSettings.FromAddress,
			Name = _emailSettings.FromName
		};

		var message = MailHelper.CreateSingleEmail(
			from, to, email.Subject, email.Body, email.Body);
		var response = await client.SendEmailAsync(message);

		return response.StatusCode == HttpStatusCode.OK ||
			response.StatusCode == HttpStatusCode.Accepted;
	}
}
