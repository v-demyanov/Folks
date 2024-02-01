// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Application.Mappings;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Moq;
using Moq.EntityFrameworkCore;

namespace Folks.ChannelsService.Application.UnitTests.Handlers;

public class GetGroupQueryHandlerTests
{
    public static IEnumerable<object[]> ValidMemberData => new List<object[]>
    {
        new object[]
        {
            new GetGroupQuery
            {
                GroupId = "65ad52c99cc26f24c9c84de0",
                CurrentUserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
            new GroupDto
            {
                Id = "65ad52c99cc26f24c9c84de0",
                Title = "Group 1",
                OwnerId = "4e5a6964-e7c8-4c60-8939-a4f982c674ad",
                CreatedAt = new DateTimeOffset(2024, 2, 18, 0, 0, 0, TimeSpan.Zero),
                Members = new List<UserDto>
                {
                    new UserDto
                    {
                        Id = "0bc02651-7684-407f-9dae-99881b42912d",
                        UserName = "Vlad",
                        Email = "v.demyanov@gmail.com",
                    },
                    new UserDto
                    {
                        Id = "4e5a6964-e7c8-4c60-8939-a4f982c674ad",
                        UserName = "Ildar",
                        Email = "ildar@gmail.com",
                    },
                },
            },
        },
    };

    [Theory]
    [MemberData(nameof(ValidMemberData))]
    public async Task Handle_ValidQuery_ShouldReturnExpectedResult(GetGroupQuery query, GroupDto expectedResult)
    {
        // Arrange
        var handler = ArrangeHandler();

        // Act
        var actualResult = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equivalent(expectedResult, actualResult);
    }

    private static GetGroupQueryHandler ArrangeHandler()
    {
        var dbContextMock = ArrangeDbContextMock();
        var mapper = ArrangeMapper(dbContextMock);

        return new GetGroupQueryHandler(dbContextMock, mapper);
    }

    private static ChannelsServiceDbContext ArrangeDbContextMock()
    {
        var dbContextMock = new Mock<ChannelsServiceDbContext>();

        dbContextMock.SetupGet(x => x.Users).ReturnsDbSet(GetUsersMock());
        dbContextMock.SetupGet(x => x.Groups).ReturnsDbSet(GetGroupsMock());

        return dbContextMock.Object;
    }

    private static IMapper ArrangeMapper(ChannelsServiceDbContext dbContextMock)
    {
        var services = new ServiceCollection();

        services.AddTransient(serviceProvider => dbContextMock);
        services.AddAutoMapper(typeof(MappingProfile));

        return services.BuildServiceProvider()
            .GetRequiredService<IMapper>();
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
            GroupIds = new List<ObjectId>()
            {
                ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            },
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
                ObjectId.Parse("65ad52f4bb11cf15a9dc1470"),
            },
            CreatedAt = new DateTimeOffset(2024, 2, 18, 0, 0, 0, TimeSpan.Zero),
            Title = "Group 1",
            OwnerId = ObjectId.Parse("65ad52f4bb11cf15a9dc1470"),
        },
    };
}
