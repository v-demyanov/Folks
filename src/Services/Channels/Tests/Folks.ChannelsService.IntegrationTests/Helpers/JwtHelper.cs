// Copyright (c) v-demyanov. All rights reserved.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Folks.ChannelsService.IntegrationTests.Constants;

using Microsoft.IdentityModel.Tokens;

namespace Folks.ChannelsService.IntegrationTests.Helpers;

public static class JwtHelper
{
    public static string CreateAccessJwt(string userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var signingCredentials = new SigningCredentials(TestJwtConstants.Key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(TestJwtConstants.AccessTokenExpires)),
            issuer: TestJwtConstants.TestIssuer,
            audience: TestJwtConstants.TestAudience,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static bool ValidateJwt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = GetValidationParameters();

        try
        {
            tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var currentDate = new DateTimeOffset(DateTime.UtcNow);

            return securityToken.ValidTo > currentDate;
        }
        catch (SecurityTokenValidationException)
        {
            return false;
        }
    }

    public static TokenValidationParameters GetValidationParameters() =>
        new ()
        {
            RequireExpirationTime = true,
            ValidateIssuer = true,
            ValidIssuer = TestJwtConstants.TestIssuer,
            ValidateAudience = true,
            ValidAudience = TestJwtConstants.TestAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = TestJwtConstants.Key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

    public static ClaimsPrincipal GetClaimsPrincipal(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadToken(token) as JwtSecurityToken;

        var claimsIdentity = new ClaimsIdentity(jwtSecurityToken?.Claims, "Test");
        return new ClaimsPrincipal(claimsIdentity);
    }
}
