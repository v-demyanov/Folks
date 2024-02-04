// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.IntegrationTests.Clients;
using Folks.ChannelsService.IntegrationTests.Factories.Contracts;

namespace Folks.ChannelsService.IntegrationTests.Factories;

public class GroupsControllerClientFactory : ControllerClientFactory<GroupsControllerClient>
{
    public GroupsControllerClientFactory(ChannelsWebApplicationFactory webApplicationFactory)
        : base(webApplicationFactory)
    {
    }

    protected override GroupsControllerClient CreateControllerClient(HttpClient httpClient) =>
        new (httpClient);
}
