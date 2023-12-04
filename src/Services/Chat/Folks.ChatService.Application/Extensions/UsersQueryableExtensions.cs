using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Extensions;

public static class UsersQueryableExtensions
{
    public static IQueryable<User> GetBySourceIds(this IQueryable<User> users, IEnumerable<string> sourceIds) =>
        users.Where(user => sourceIds.Any(userId => userId == user.SourceId));
}
