// Custom Authentication Handler

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class NoValidationAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public NoValidationAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // You can check if a token is present in the header, but don't validate it.
        // For example, if (Request.Headers.ContainsKey("Authorization"))
        // {
        //     // Optionally extract and log the token, but don't validate its signature or claims.
        // }

        var identity = new ClaimsIdentity("NoValidationAuth");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "NoValidationAuth");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}