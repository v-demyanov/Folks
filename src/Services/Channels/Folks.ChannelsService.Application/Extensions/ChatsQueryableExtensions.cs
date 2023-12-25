using MongoDB.Bson;

using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Extensions;

public static class ChatsQueryableExtensions
{
    // TODO: Replace it on IQueryable after mongo-efcore-provider will be updated
    public static IEnumerable<Chat> GetByIds(this IQueryable<Chat> chats, IEnumerable<ObjectId> ids) =>
        chats.AsEnumerable()
            .Where(chat => ids.Any(chatId => chatId == chat.Id));
}
