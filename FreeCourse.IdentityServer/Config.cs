﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = {"catalog_fullpermission" } },
            new ApiResource("photo_stock_catalog"){Scopes = { "photo_stock_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name = "roles",DisplayName ="Roles", Description = "Kullanıcı rolleri", UserClaims = new []{ "roles"} }

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                // Burada write read full permission gibi eklemeler yapılır. Bu scopelara göre erişim sağlar clientler.
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("catalog_write","Catalog API için full erişim"),
                new ApiScope("catalog_read","Catalog API için full erişim"),

                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope("photo_stock_write","Photo Stock API için full erişim"),
                new ApiScope("photo_stock_read","Photo Stock API için full erişim"),

                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
              new Client
              {
                  // Client oluşturduk ve bu clientin şifresini granttypesini ıd sini ve izinlerini belirttik.
                  ClientName = "Asp.Net Core MVC",
                  ClientId = "WebMvcClient",
                  ClientSecrets={new Secret("secret".Sha256())},
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  AllowedScopes= { "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
              }
            };
    }
}