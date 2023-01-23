// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.AspNetCore.Authentication.Kakao;
public static class KakaoDefaults
{
    ///<summary>
    /// The default scheme for Kakao authentication. Defaults to <c>Kakao</c>.
    /// </summary>
    public const string AuthenticationScheme = "Kakao";

    /// <summary>
    /// The default display name for Kakao authentication. Defaults to <c>Kakao</c>.
    /// </summary>
    public static readonly string DisplayName = "Kakao";

    /// <summary>
    /// The default endpoint used to perform Kakao authentication.
    /// </summary>
    public static readonly string AuthorizationEndpoint = "https://kauth.kakao.com/oauth/authorize";

    /// <summary>
    /// The OAuth endpoint used to exchange access tokens.
    /// </summary>
    public static readonly string TokenEndpoint = "https://kauth.kakao.com/oauth/token";

    /// <summary>
    /// The Kakao endpoint that is used to gather additional user information.
    /// </summary>
    public static readonly string UserInformationEndpoint = "https://kapi.kakao.com//v2/user/me";
}
