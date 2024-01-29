// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Entities;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Extensions;

public static class MessagesQueryableExtensions
{
    public static IQueryable<Message> GetByGroupId(this IQueryable<Message> messages, ObjectId groupId) =>
        messages.Where(message => message.GroupId.HasValue && message.GroupId.Value == groupId);

    public static IQueryable<Message> GetByChatId(this IQueryable<Message> messages, ObjectId chatId) =>
        messages.Where(message => message.ChatId.HasValue && message.ChatId.Value == chatId);

    // TODO: Replace it on IQueryable after mongo-efcore-provider will be updated
    public static IEnumerable<Message> GetByIds(this IQueryable<Message> messages, IEnumerable<string> ids) =>
        messages.AsEnumerable()
            .Where(message => ids.Any(id => id == message.Id.ToString()));
}
