// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Extensions;

public static class ValidatorsExtensions
{
    public static IRuleBuilderOptions<T, string?> UserMustExist<T>(this IRuleBuilder<T, string?> rule, ChannelsServiceDbContext dbContext) =>
        rule.Must(userId => dbContext.Users.Any(user => user.SourceId == userId))
            .WithMessage((model, userId) => $"The user with id=\"{userId}\" doesn't exist.");

    public static IRuleBuilderOptions<T, ChannelMustExistCustomValidatorProperty> ChannelMustExist<T>(this IRuleBuilder<T, ChannelMustExistCustomValidatorProperty> rule, ChannelsServiceDbContext dbContext) =>
        rule.Must((model, property) =>
                property.ChannelType switch
                {
                    ChannelType.Chat => dbContext.Chats.Any(chat => chat.Id.ToString() == property.ChannelId),
                    ChannelType.Group => dbContext.Groups.Any(group => group.Id.ToString() == property.ChannelId),
                    _ => false,
                })
            .WithMessage((model, property) => $"The channel with id=\"{property.ChannelId}\" doesn't exist.");
}
