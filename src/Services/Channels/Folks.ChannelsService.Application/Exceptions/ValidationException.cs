using FluentValidation.Results;

namespace Folks.ChannelsService.Application.Exceptions;

public class ValidationException : Exception
{
    private const string _message = "Validation failed.";

    public ValidationException() 
        : base(_message)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base(_message)
    {
        Errors = failures
            .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}
