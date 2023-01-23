// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeRabbits.AspNetCore.Authentication.Naver;

/// <summary>
/// Configuration options for <see cref="NaverHandler"/>.
/// </summary>
public class NaverOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new <see cref="NaverOptions"/>.
    /// </summary>
    public NaverOptions()
    {
        CallbackPath = new PathString("/signin-naver");
        AuthorizationEndpoint = NaverDefaults.AuthorizationEndpoint;
        TokenEndpoint = NaverDefaults.TokenEndpoint;
        UserInformationEndpoint = NaverDefaults.UserInformationEndpoint;

        ClaimActions.MapJsonSubKey(ClaimTypes.NameIdentifier, "response", "id");
        ClaimActions.MapJsonSubKey(ClaimTypes.Name, "response", "name");
        ClaimActions.MapJsonSubKey(ClaimTypes.Email, "response", "email");
    }
}
