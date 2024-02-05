// Copyright (c) v-demyanov. All rights reserved.

using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.IntegrationTests.Fixtures.Controllers;

namespace Folks.ChannelsService.IntegrationTests.Tests.Controllers.Groups;

public class GetGroupEndpointTests : IClassFixture<GroupsControllerTestFixture>
{
    private readonly GroupsControllerTestFixture fixture;

    public GetGroupEndpointTests(GroupsControllerTestFixture fixture)
    {
        this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

    public static IEnumerable<object[]> ValidRequestMemberData => new List<object[]>
    {
        new object[]
        {
            "86b60e2a-8e2a-4fb3-944d-3fcba7d1255f",
            "65ad52c99cc26f24c9c84de0",
            new GroupDto
            {
                Id = "65ad52c99cc26f24c9c84de0",
                Title = "Group 1",
                OwnerId = "86b60e2a-8e2a-4fb3-944d-3fcba7d1255f",
                CreatedAt = new DateTimeOffset(2024, 2, 18, 0, 0, 0, TimeSpan.Zero),
                Members = new List<UserDto>
                {
                    new UserDto
                    {
                        Id = "86b60e2a-8e2a-4fb3-944d-3fcba7d1255f",
                        UserName = "Vlad",
                        Email = "v.demyanov.dev@gmail.com",
                    },
                },
            },
        },
    };

    [Fact]
    public async Task GetEndpoint_AnonymousUser_ShouldReturnUnauthorized()
    {
        // Arrange
        var client = this.fixture.ControllerClientFactory.CreateAnonymousClient();
        var groupId = "65ad52c99cc26f24c9c84de0";

        // Act
        var actualResponse = await client.GetAsync(groupId);

        // Assert
        Assert.False(actualResponse.IsSuccessStatusCode);
        Assert.True(actualResponse.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Theory]
    [MemberData(nameof(ValidRequestMemberData))]
    public async Task GetEndpoint_ValidRequest_ShouldReturnExpectedResponse(string currentUserId, string groupId, GroupDto expectedBody)
    {
        // Arrange
        var client = this.fixture.ControllerClientFactory.CreateAuthorizedClient(currentUserId);

        // Act
        var actualResponse = await client.GetAsync(groupId);
        var actualBody = await actualResponse.Content.ReadFromJsonAsync<GroupDto>();

        // Assert
        Assert.True(actualResponse.IsSuccessStatusCode);
        Assert.True(actualResponse.StatusCode == HttpStatusCode.OK);

        actualBody.Should()
            .BeEquivalentTo(expectedBody, options => options.Excluding(x => x.CreatedAt));
    }
}
