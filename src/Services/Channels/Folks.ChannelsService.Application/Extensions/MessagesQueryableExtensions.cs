using Folks.ChannelsService.Domain.Entities;
using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Extensions;

public static class MessagesQueryableExtensions
{
    public static IQueryable<Message> GetByGroupId(this IQueryable<Message> messages, ObjectId groupId) =>
        messages.Where(message => message.GroupId.HasValue && message.GroupId.Value == groupId);

    public static IQueryable<Message> GetByChatId(this IQueryable<Message> messages, ObjectId chatId) =>
        messages.Where(message => message.ChatId.HasValue && message.ChatId.Value == chatId);
}
