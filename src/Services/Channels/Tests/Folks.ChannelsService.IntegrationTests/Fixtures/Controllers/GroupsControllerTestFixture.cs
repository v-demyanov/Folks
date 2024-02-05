// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.IntegrationTests.Clients;
using Folks.ChannelsService.IntegrationTests.Factories;
using Folks.ChannelsService.IntegrationTests.Factories.Contracts;
using Folks.ChannelsService.IntegrationTests.Fixtures.Contracts;

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace Folks.ChannelsService.IntegrationTests.Fixtures.Controllers;

public class GroupsControllerTestFixture : ControllerTestFixture<GroupsControllerClient>
{
    public GroupsControllerTestFixture()
    {
        this.InitializeDatabase();
    }

    public override ControllerClientFactory<GroupsControllerClient> ControllerClientFactory =>
        new GroupsControllerClientFactory(this.WebApplicationFactory);

    private static IEnumerable<User> GetInitialUsers() => new List<User>
    {
        new User
        {
            Id = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            SourceId = "86b60e2a-8e2a-4fb3-944d-3fcba7d1255f",
            UserName = "Vlad",
            Email = "v.demyanov.dev@gmail.com",
            ChatIds = new List<ObjectId>(),
            GroupIds = new List<ObjectId>
            {
                ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            },
        },
    };

    private static IEnumerable<Group> GetInitialGroups() => new List<Group>
    {
        new Group
        {
            Id = ObjectId.Parse("65ad52c99cc26f24c9c84de0"),
            UserIds = new List<ObjectId>
            {
                ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
            },
            Title = "Group 1",
            OwnerId = ObjectId.Parse("65ad52e4eb97a3806c6bdf92"),
        },
    };

    private void InitializeDatabase()
    {
        using (var scope = this.WebApplicationFactory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<ChannelsServiceDbContext>();

            dbContext.Users.AddRange(GetInitialUsers());
            dbContext.Groups.AddRange(GetInitialGroups());
            dbContext.SaveChanges();
        }
    }
}
