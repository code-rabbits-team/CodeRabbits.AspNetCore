// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CodeRabbits.AspNetCore.Authentication.Kakao;

/// <summary>
/// Authentication handler for Kakao's OAuth based authentication.
/// </summary>
public class KakaoHandler : OAuthHandler<KakaoOptions>
{

    /// <summary>
    /// Initializes a new instance of <see cref="KakaoHandler"/>.
    /// </summary>
    /// <inheritdoc />
    public KakaoHandler(IOptionsMonitor<KakaoOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    /// <inheritdoc />
    protected override async Task<AuthenticationTicket> CreateTicketAsync(
        ClaimsIdentity identity,
        AuthenticationProperties properties,
        OAuthTokenResponse tokens)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var response = await Backchannel.SendAsync(request, Context.RequestAborted);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"An error occurred when retrieving Kakao user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Kakao login is activation.");
        }

        using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync(Context.RequestAborted));
        var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
        context.RunClaimActions();
        await Events.CreatingTicket(context);
        return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
    }

    /// <inheritdoc />
    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "response_type", "code" },
            { "client_id", Options.ClientId },
            { "redirect_uri", redirectUri }
        };

        AddQueryString(queryStrings, properties, KakaoChallengeProperties.PromptParameterKey);
        AddQueryString(queryStrings, properties, KakaoChallengeProperties.NonceParameterKey);

        var state = Options.StateDataFormat.Protect(properties);
        queryStrings.Add("state", state);        

        var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings!);
        return authorizationEndpoint;
    }

    private static void AddQueryString<T>(
        IDictionary<string, string> queryStrings,
        AuthenticationProperties properties,
        string name,
        Func<T, string?> formatter,
        T defaultValue)
    {
        string? value;
        var parameterValue = properties.GetParameter<T>(name);
        if (parameterValue != null)
        {
            value = formatter(parameterValue);
        }
        else if (!properties.Items.TryGetValue(name, out value))
        {
            value = formatter(defaultValue);
        }

        properties.Items.Remove(name);

        if (value != null)
        {
            queryStrings[name] = value;
        }
    }

    private static void AddQueryString(
        IDictionary<string, string> queryStrings,
        AuthenticationProperties properties,
        string name,
        string? defaultValue = null)
        => AddQueryString(queryStrings, properties, name, x => x, defaultValue);
}
