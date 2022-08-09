namespace CleanArchApi.Application.Exceptions;

using FluentValidation.Results;

public class ValidationException : ApplicationException
{
	public List<string> Errors { get; set; } = new List<string>();

	public ValidationException(ValidationResult result)
	{
		foreach (var error in result.Errors)
		{
			Errors.Add(error.ErrorMessage);
		}
	}
}
