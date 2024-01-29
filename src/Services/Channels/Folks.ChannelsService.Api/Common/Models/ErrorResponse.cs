// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Api.Common.Models;

public record class ErrorResponse
{
    public int StatusCode { get; init; }

    required public string Title { get; init; }

    public object? Errors { get; init; }
}
