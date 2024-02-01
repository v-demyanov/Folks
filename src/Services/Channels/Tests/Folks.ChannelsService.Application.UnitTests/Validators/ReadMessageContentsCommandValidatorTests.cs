// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation.Results;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;
using Folks.ChannelsService.Domain.Common.Enums;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.Tests.Common.Contracts;

using MongoDB.Bson;
using Moq;
using Moq.EntityFrameworkCore;

namespace Folks.ChannelsService.Application.UnitTests.Validators;

public class ReadMessageContentsCommandValidatorTests : ValidatorTestsBase<ReadMessageContentsCommand, ReadMessageContentsCommandValidator>
{
    public static IEnumerable<object[]> InvalidMemberData => new List<object[]>
    {
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "Invalid message id",
                },
                ChannelId = "65ad52c99cc26f24c9c84de0",
                ChannelType = ChannelType.Group,
                UserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
            new ValidationFailure[]
            {
                new ValidationFailure("MessageIds", "Some messages don't exist."),
            },
        },
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "65ad544eeeadab406e8486ce",
                },
                ChannelId = "Invalid channel id",
                ChannelType = ChannelType.Group,
                UserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
            new ValidationFailure[]
            {
                new ValidationFailure(string.Empty, "The channel with id=\"Invalid channel id\" doesn't exist."),
            },
        },
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "65ad544eeeadab406e8486ce",
                },
                ChannelId = "65ad52c99cc26f24c9c84de0",
                ChannelType = ChannelType.Group,
                UserId = "Invalid user id",
            },
            new ValidationFailure[]
            {
                new ValidationFailure("UserId", "The user with id=\"Invalid user id\" doesn't exist."),
            },
        },
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "65ad544eeeadab406e8486ce",
                },
                ChannelId = string.Empty,
                ChannelType = ChannelType.Group,
                UserId = string.Empty,
            },
            new ValidationFailure[]
            {
                new ValidationFailure("UserId", "'User Id' must not be empty."),
                new ValidationFailure("UserId", "The user with id=\"\" doesn't exist."),
                new ValidationFailure("ChannelId", "'Channel Id' must not be empty."),
                new ValidationFailure(string.Empty, "The channel with id=\"\" doesn't exist."),
            },
        },
    };

    public static IEnumerable<object[]> ValidMemberData => new List<object[]>
    {
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "65ad54418027e89670d7f67e",
                },
                ChannelId = "65ad52c99cc26f24c9c84de0",
                ChannelType = ChannelType.Group,
                UserId = "0bc02651-7684-407f-9dae-99881b42912d",
            },
        },
        new object[]
        {
            new ReadMessageContentsCommand
            {
                MessageIds = new List<string>
                {
                    "65ad5452166ff2f1b463c1f1",
                },
                ChannelId = "65ad52c99cc26f24c9c84de0",
                ChannelType = ChannelType.Group,
                UserId = "4e5a6964-e7c8-4c60-8939-a4f982c674ad",
            },
        },
    };

    protected override ReadMessageContentsCommandValidator ArrangeValidator()
    {
        var dbContextMock = ArrangeDbContextMock();
        var validator = new ReadMessageContentsCommandValidator(dbContextMock);

        return validator;
    }

    private static ChannelsServiceDbContext ArrangeDbContextMock()
    {
        var dbContextMock = new Mock<ChannelsServiceDbContext>();

        dbContextMock.SetupGet(x => x.Users).ReturnsDbSet(GetUsersMock());
        dbContextMock.SetupGet(x => x.Messages).ReturnsDbSet(GetMessagesMock());
        dbContextMock.SetupGet(x => x.Chats).ReturnsDbSet(GetChatsMock());
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
            GroupIds = new List<ObjectId>()
            {
                ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            },
        },
    };

    private static IEnumerable<Message> GetMessagesMock() => new List<Message>
    {
        new Message
        {
            Id = ObjectId.Parse("65ad54418027e89670d7f67e"),
            Content = "Message 1",
            SentAt = DateTimeOffset.Now,
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            GroupId = ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            Type = MessageType.Text,
            ReadByIds = new List<ObjectId>(),
        },
        new Message
        {
            Id = ObjectId.Parse("65ad544eeeadab406e8486ce"),
            Content = "Message 2",
            SentAt = DateTimeOffset.Now,
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            GroupId = ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            Type = MessageType.Text,
            ReadByIds = new List<ObjectId>(),
        },
        new Message
        {
            Id = ObjectId.Parse("65ad5452166ff2f1b463c1f1"),
            Content = "Message 3",
            SentAt = DateTimeOffset.Now,
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            GroupId = ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            Type = MessageType.Text,
            ReadByIds = new List<ObjectId>(),
        },
    };

    private static IEnumerable<Chat> GetChatsMock() => new List<Chat>();

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
            CreatedAt = DateTimeOffset.Now,
            Title = "Group 1",
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
        },
    };
}
