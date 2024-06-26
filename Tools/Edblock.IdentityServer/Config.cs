﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using Edblock.Library.Constants;
using System.Collections.Generic;

namespace Edblock.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
                new ApiScope(IdConstants.ApiScope),
                new ApiScope(IdConstants.WebScope),
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "test.client",
                ClientName = "Test client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = 
                { 
                    new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdConstants.WebScope,
                    IdConstants.ApiScope
                }
            },

            new Client
            {
                ClientId = "desktop.client",
                ClientName = "Desktop client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = 
                { 
                    new Secret("QrkrroMyiGt7XJ9fwYeB0ZKW8GoLXHujVwQ=".Sha256())
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdConstants.WebScope,
                    IdConstants.ApiScope
                }
            },

            new Client
            {
                ClientId = "external",
                ClientName = "External Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdConstants.WebScope
                }
            }
        ];
}