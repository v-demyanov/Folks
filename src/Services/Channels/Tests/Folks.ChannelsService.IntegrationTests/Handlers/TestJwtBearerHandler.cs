// Copyright (c) v-demyanov. All rights reserved.

using System.Text.Encodings.Web;

using Folks.ChannelsService.IntegrationTests.Constants;
using Folks.ChannelsService.IntegrationTests.Helpers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Folks.ChannelsService.IntegrationTests.Handlers;

public class TestJwtBearerHandler : JwtBearerHandler
{
    public TestJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!this.Context.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues))
        {
            var failedResult = AuthenticateResult.Fail("Authorization header not found.");
            return Task.FromResult(failedResult);
        }

        var authorizationHeader = authorizationHeaderValues.FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            var failedResult = AuthenticateResult.Fail("Bearer token not found in Authorization header.");
            return Task.FromResult(failedResult);
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        if (!JwtHelper.ValidateJwt(token))
        {
            var failedResult = AuthenticateResult.Fail("Token validation failed.");
            return Task.FromResult(failedResult);
        }

        var principal = JwtHelper.GetClaimsPrincipal(token);
        var ticket = new AuthenticationTicket(principal, TestJwtConstants.TestJwtScheme);
        var successResult = AuthenticateResult.Success(ticket);

        return Task.FromResult(successResult);
    }
}
