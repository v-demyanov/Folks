// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Constants;
using Folks.Tests.Common.Contracts;

namespace Folks.ChannelsService.IntegrationTests.Clients;

public class GroupsControllerClient : BaseControllerClient
{
    public GroupsControllerClient(HttpClient httpClient)
        : base(httpClient, ApiRoutePatterns.GroupsController)
    {
    }

    public async Task<HttpResponseMessage> GetAsync(string groupId) =>
        await this.HttpClient.GetAsync($"{this.BaseRoute}/{groupId}");
}
