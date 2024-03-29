﻿// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using MediatR;

namespace Folks.ChannelsService.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(this.validators.Select(validator => validator.ValidateAsync(context)));

        var validationFalilures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors);

        if (validationFalilures.Any())
        {
            throw new Exceptions.ValidationException(validationFalilures);
        }

        return await next();
    }
}
