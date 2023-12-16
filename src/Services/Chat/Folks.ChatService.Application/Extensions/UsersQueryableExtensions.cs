using MongoDB.Bson;

using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Extensions;

public static class UsersQueryableExtensions
{
    public static IQueryable<User> GetBySourceIds(this IQueryable<User> users, IEnumerable<string> sourceIds) =>
        users.Where(user => sourceIds.Any(userId => userId == user.SourceId));

    public static User GetBySourceId(this IQueryable<User> users, string sourceId) =>
        users.First(users => users.SourceId == sourceId);

    public static User GetById(this IQueryable<User> users, ObjectId id) =>
        users.First(users => users.Id == id);
}
