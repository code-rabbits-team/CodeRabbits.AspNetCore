// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeRabbits.AspNetCore.Authentication.Kakao;

public class KakaoOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new <see cref="KakaoOptions"/>.
    /// </summary>
    public KakaoOptions()
    {
        CallbackPath = new PathString("/signin-kakao");
        AuthorizationEndpoint = KakaoDefaults.AuthorizationEndpoint;
        TokenEndpoint = KakaoDefaults.TokenEndpoint;
        UserInformationEndpoint = KakaoDefaults.UserInformationEndpoint;

        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        ClaimActions.MapJsonThirdKey(ClaimTypes.Email, "properties", "kakao_account", "email");
    }
}
