namespace CleanArchApi.Application.UnitTests.Mocks.Infrastructure;

using Application.Contracts.Infrastructure;
using Application.Models;

public static class MockEmailSender
{
	public static Mock<IEmailSender> GetMock()
	{
		var mockEmailSender = new Mock<IEmailSender>();
		mockEmailSender.Setup(e => e.SendEmail(It.IsAny<Email>())).ReturnsAsync(true);

		return mockEmailSender;
	}
}
