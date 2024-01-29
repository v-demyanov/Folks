// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation.Results;

namespace Folks.ChannelsService.Application.Exceptions;

public class ValidationException : Exception
{
    private const string MessageText = "Validation failed.";

    public ValidationException()
        : base(MessageText)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base(MessageText)
    {
        this.Errors = failures
            .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}
