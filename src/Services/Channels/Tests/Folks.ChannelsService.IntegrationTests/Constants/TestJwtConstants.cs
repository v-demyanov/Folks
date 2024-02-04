// Copyright (c) v-demyanov. All rights reserved.

using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace Folks.ChannelsService.IntegrationTests.Constants;

public static class TestJwtConstants
{
    public const string TestJwtScheme = "TestJwtBearer";

    public const string TestIssuer = "TestIssuer";

    public const string TestAudience = "TestAudience";

    /// <summary>
    /// Time presented in minutes.
    /// </summary>
    public const int AccessTokenExpires = 120;

    public static SymmetricSecurityKey Key { get; } = new (Encoding.UTF8.GetBytes("d61f994d6e6c02f2c8b0fabbd18274ee"));
}
