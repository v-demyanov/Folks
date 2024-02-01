// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Entities;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Extensions;

public static class GroupsQueryableExtensions
{
    public static Group GetById(this IQueryable<Group> groups, ObjectId id) =>
        groups.First(group => group.Id == id);

    public static Group GetById(this IQueryable<Group> groups, string id) =>
        groups.First(group => group.Id.ToString() == id);

    // TODO: Replace it on IQueryable after mongo-efcore-provider will be updated
    public static IEnumerable<Group> GetByIds(this IQueryable<Group> groups, IEnumerable<ObjectId> ids) =>
        groups.AsEnumerable()
            .Where(group => ids.Any(groupId => groupId == group.Id));
}
