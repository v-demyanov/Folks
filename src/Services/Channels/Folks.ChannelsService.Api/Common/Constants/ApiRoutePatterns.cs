﻿// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Api.Common.Constants;

public static class ApiRoutePatterns
{
    public const string BaseRoute = "api/v1/";

    public const string ChannelsController = $"{BaseRoute}channels";

    public const string GroupsController = $"{ChannelsController}/groups";

    public const string MessagesController = $"{ChannelsController}/{{channelId}}/messages";

    public const string ChannelsHub = $"{BaseRoute}hubs/channels";
}
