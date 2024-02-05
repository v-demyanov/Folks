// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.IntegrationTests.Helpers;
using Folks.Tests.Common.Contracts;

namespace Folks.ChannelsService.IntegrationTests.Factories.Contracts;

public abstract class ControllerClientFactory<TControllerClient>
    where TControllerClient : BaseControllerClient
{
    private readonly ChannelsWebApplicationFactory webApplicationFactory;

    protected ControllerClientFactory(ChannelsWebApplicationFactory webApplicationFactory)
    {
        this.webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
    }

    public virtual TControllerClient CreateAnonymousClient()
    {
        var httpClient = this.webApplicationFactory.CreateClient();
        return this.CreateControllerClient(httpClient);
    }

    public virtual TControllerClient CreateAuthorizedClient(string userId)
    {
        var httpClient = this.webApplicationFactory.CreateClient();

        var accessToken = JwtHelper.CreateAccessJwt(userId);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        return this.CreateControllerClient(httpClient);
    }

    protected abstract TControllerClient CreateControllerClient(HttpClient httpClient);
}
