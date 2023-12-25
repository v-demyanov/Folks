using MongoDB.Bson;

using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Extensions;

public static class UsersQueryableExtensions
{
    public static IQueryable<User> GetBySourceIds(this IQueryable<User> users, IEnumerable<string> sourceIds) =>
        users.Where(user => sourceIds.Any(userId => userId == user.SourceId));

    public static User GetBySourceId(this IQueryable<User> users, string sourceId) =>
        users.First(user => user.SourceId == sourceId);

    public static User GetById(this IQueryable<User> users, ObjectId id) =>
        users.First(user => user.Id == id);

    // TODO: Replace it on IQueryable after mongo-efcore-provider will be updated
    public static IEnumerable<User> GetByGroupId(this IQueryable<User> users, ObjectId groupId) =>
        users.AsEnumerable()
            .Where(user => user.GroupIds.Any(id => id == groupId));

    // TODO: Replace it on IQueryable after mongo-efcore-provider will be updated
    public static IEnumerable<User> GetByChatId(this IQueryable<User> users, ObjectId chatId) =>
        users.AsEnumerable()
            .Where(user => user.ChatIds.Any(id => id == chatId));
}
