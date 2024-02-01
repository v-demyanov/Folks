// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation.Results;

using Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.Tests.Common.Contracts;

using MongoDB.Bson;
using Moq;
using Moq.EntityFrameworkCore;

namespace Folks.ChannelsService.Application.UnitTests.Validators;

public class GetGroupQueryValidatorTests : ValidatorTestsBase<GetGroupQuery, GetGroupQueryValidator>
{
    public static IEnumerable<object[]> InvalidMemberData => new List<object[]>
    {
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = "Invalid group id",
                CurrentUserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
            new ValidationFailure[]
            {
                new ValidationFailure("CurrentUserId", "User with id=\"0bc02651-7684-407f-9dae-99881b42912d\" haven't joined this group."),
                new ValidationFailure(string.Empty, "The channel with id=\"Invalid group id\" doesn't exist."),
            },
        },
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = "65ad52c99cc26f24c9c84de0",
                CurrentUserId = "Invalid user id",
            },
            new ValidationFailure[]
            {
                new ValidationFailure("CurrentUserId", "The user with id=\"Invalid user id\" doesn't exist."),
                new ValidationFailure("CurrentUserId", "User with id=\"Invalid user id\" haven't joined this group."),
            },
        },
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = "65ad52c99cc26f24c9c84de0",
                CurrentUserId = "4e5a6964-e7c8-4c60-8939-a4f982c674ad",
            },
            new ValidationFailure[]
            {
                new ValidationFailure("CurrentUserId", "User with id=\"4e5a6964-e7c8-4c60-8939-a4f982c674ad\" haven't joined this group."),
            },
        },
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = string.Empty,
                CurrentUserId = string.Empty,
            },
            new ValidationFailure[]
            {
                new ValidationFailure("CurrentUserId", "'Current User Id' must not be empty."),
                new ValidationFailure("CurrentUserId", "The user with id=\"\" doesn't exist."),
                new ValidationFailure("CurrentUserId", "User with id=\"\" haven't joined this group."),
                new ValidationFailure("GroupId", "'Group Id' must not be empty."),
                new ValidationFailure(string.Empty, "The channel with id=\"\" doesn't exist."),
            },
        },
    };

    public static IEnumerable<object[]> ValidMemberData => new List<object[]>
    {
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = "65ad52c99cc26f24c9c84de0",
                CurrentUserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
        },
    };

    protected override GetGroupQueryValidator ArrangeValidator()
    {
        var dbContextMock = ArrangeDbContextMock();
        var validator = new GetGroupQueryValidator(dbContextMock);

        return validator;
    }

    private static ChannelsServiceDbContext ArrangeDbContextMock()
    {
        var dbContextMock = new Mock<ChannelsServiceDbContext>();

        dbContextMock.SetupGet(x => x.Users).ReturnsDbSet(GetUsersMock());
        dbContextMock.SetupGet(x => x.Groups).ReturnsDbSet(GetGroupsMock());

        return dbContextMock.Object;
    }

    private static IEnumerable<User> GetUsersMock() => new List<User>
    {
        new User
        {
            Id = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            SourceId = "0bc02651-7684-407f-9dae-99881b42912d",
            UserName = "Vlad",
            Email = "v.demyanov@gmail.com",
            ChatIds = new List<ObjectId>(),
            GroupIds = new List<ObjectId>()
            {
                ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            },
        },
        new User
        {
            Id = ObjectId.Parse("65ad52f4bb11cf15a9dc1470"),
            SourceId = "4e5a6964-e7c8-4c60-8939-a4f982c674ad",
            UserName = "Ildar",
            Email = "ildar@gmail.com",
            ChatIds = new List<ObjectId>(),
            GroupIds = new List<ObjectId>(),
        },
    };

    private static IEnumerable<Group> GetGroupsMock() => new List<Group>
    {
        new Group
        {
            Id = ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            UserIds = new List<ObjectId>
            {
                ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            },
            CreatedAt = DateTimeOffset.Now,
            Title = "Group 1",
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
        },
    };
}
