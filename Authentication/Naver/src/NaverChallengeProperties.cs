// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication.OAuth;

namespace CodeRabbits.AspNetCore.Authentication.Naver;

/// <summary>
/// <see cref="AuthenticationProperties"/> for a Naver OAuth challenge.
/// </summary>
public class NaverChallengeProperties : OAuthChallengeProperties
{
    /// <summary>
    /// The parameter key for the "scope" argument being used for a challenge request.
    /// </summary>
    public static readonly string ScopeParameterkey = "scope";

    /// <summary>
    /// Initializes a new instance of <see cref="NaverChallengeProperties"/>.
    /// </summary>
    public NaverChallengeProperties()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="NaverChallengeProperties"/>.
    /// </summary>
    public NaverChallengeProperties(IDictionary<string, string?> items)
        : base(items)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="NaverChallengeProperties"/>.
    /// </summary>
    public NaverChallengeProperties(IDictionary<string, string?> items, IDictionary<string, object?> parameters)
        : base(items, parameters)
    { }

    /// <summary>
    /// The "scope" parameter value being used for a challenge request.
    /// </summary>
    public new string? Scope
    {
        get => GetParameter<string>(ScopeParameterkey);
        set => SetParameter(ScopeParameterkey, value);
    }
}
