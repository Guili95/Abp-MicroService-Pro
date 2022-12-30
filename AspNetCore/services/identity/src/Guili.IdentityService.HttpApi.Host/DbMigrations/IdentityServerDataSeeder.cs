﻿using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace Guili.IdentityService.DbMigrations
{
    public class IdentityServerDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public IdentityServerDataSeeder(
        IClientRepository clientRepository,
        IApiResourceRepository apiResourceRepository,
        IApiScopeRepository apiScopeRepository,
        IIdentityResourceDataSeeder identityResourceDataSeeder,
        IGuidGenerator guidGenerator,
        IPermissionDataSeeder permissionDataSeeder,
        IConfiguration configuration,
        ICurrentTenant currentTenant)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }
        public Task SeedAsync(DataSeedContext context)
        {
            return SeedAsync();
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            using (_currentTenant.Change(null))
            {
                await _identityResourceDataSeeder.CreateStandardResourcesAsync();
                await CreateApiResourcesAsync();
                await CreateApiScopesAsync();
                await CreateSwaggerClientsAsync();
                await CreateClientsAsync();
            }
        }
        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
            "email",
            "email_verified",
            "name",
            "phone_number",
            "phone_number_verified",
            "role"
        };

            await CreateApiResourceAsync("AccountService", commonApiUserClaims);
            await CreateApiResourceAsync("IdentityService", commonApiUserClaims);
            await CreateApiResourceAsync("AdministrationService", commonApiUserClaims);
            await CreateApiResourceAsync("SaasService", commonApiUserClaims);
            await CreateApiResourceAsync("BackendAdminAppGateway", commonApiUserClaims);
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync("AccountService");
            await CreateApiScopeAsync("IdentityService");
            await CreateApiScopeAsync("AdministrationService");
            await CreateApiScopeAsync("SaasService");
            await CreateApiScopeAsync("BackendAdminAppGateway");
        }

        private async Task CreateSwaggerClientsAsync()
        {
            await CreateBackendAdminAppGatewaySwaggerClientAsync("BackendAdminAppGateway",
                new[]
                {
                    "AccountService",
                    "IdentityService",
                    "AdministrationService",
                    "BackendAdminAppGateway",
                    "SaasService"
                });
        }

        private async Task CreateBackendAdminAppGatewaySwaggerClientAsync(string name, string[] scopes = null)
        {
            var commonScopes = new[]
            {
            "email",
            "openid",
            "profile",
            "role",
            "phone",
            "address"
        };
            scopes ??= new[] { name };

            // Swagger Client
            var swaggerClientId = $"{name}_Swagger";
            if (!swaggerClientId.IsNullOrWhiteSpace())
            {
                var backendAdminAppGatewaySwaggerRootUrl = _configuration[$"IdentityServerClients:{name}:RootUrl"].TrimEnd('/');
                var accountServiceRootUrl = _configuration[$"IdentityServerClients:AccountService:RootUrl"].TrimEnd('/');
                var identityServiceRootUrl = _configuration[$"IdentityServerClients:IdentityService:RootUrl"].TrimEnd('/');
                var administrationServiceRootUrl = _configuration[$"IdentityServerClients:AdministrationService:RootUrl"].TrimEnd('/');
                var saasServiceRootUrl = _configuration[$"IdentityServerClients:SaasService:RootUrl"].TrimEnd('/');

                await CreateClientAsync(
                        name: swaggerClientId,
                        scopes: commonScopes.Union(scopes),
                        grantTypes: new[] { "authorization_code" },
                        secret: "1q2w3e*".Sha256(),
                        requireClientSecret: false,
                        redirectUris: new List<string>
                        {
                            $"{backendAdminAppGatewaySwaggerRootUrl}/swagger/oauth2-redirect.html", // BackendAdminAppGateway redirect uri
                            $"{accountServiceRootUrl}/swagger/oauth2-redirect.html", // AccountService redirect uri
                            $"{identityServiceRootUrl}/swagger/oauth2-redirect.html", // IdentityService redirect uri
                            $"{administrationServiceRootUrl}/swagger/oauth2-redirect.html", // AdministrationService redirect uri
                            $"{saasServiceRootUrl}/swagger/oauth2-redirect.html", // SaasService redirect uri
                        },
                        corsOrigins: new[]
                        {
                            backendAdminAppGatewaySwaggerRootUrl.RemovePostFix("/"),
                            accountServiceRootUrl.RemovePostFix("/"),
                            identityServiceRootUrl.RemovePostFix("/"),
                            administrationServiceRootUrl.RemovePostFix("/"),
                            saasServiceRootUrl.RemovePostFix("/"),
                        }
                    );
            }
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task<ApiScope> CreateApiScopeAsync(string name)
        {
            var apiScope = await _apiScopeRepository.FindByNameAsync(name);
            if (apiScope == null)
            {
                apiScope = await _apiScopeRepository.InsertAsync(
                    new ApiScope(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            return apiScope;
        }

        private async Task CreateClientsAsync()
        {
            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };

            //ABP MicroService Pro Admin Client
            var ABPMicroServiceProAdminClientRootUrl = _configuration["IdentityServerClients:ABPMicroServiceProAdmin:RootUrl"].EnsureEndsWith('/');
            await CreateClientAsync(
                name: "ABPMicroServiceProAdmin",
                scopes: commonScopes.Union(new[]
                {
                    "AccountService",
                    "IdentityService",
                    "AdministrationService",
                    "BackendAdminAppGateway",
                    "SaasService"
                }),
                grantTypes: new[] { "authorization_code" },
                secret: "1q2w3e*".Sha256(),
                requirePkce: true,
                requireClientSecret: false,
                redirectUris: new List<string> { $"{ABPMicroServiceProAdminClientRootUrl}#/oidcSignIn" },
                postLogoutRedirectUri: $"{ABPMicroServiceProAdminClientRootUrl}",
                corsOrigins: new[] { ABPMicroServiceProAdminClientRootUrl.RemovePostFix("/") }
            );

            await CreateClientAsync(
                name: "Guili_AdministrationService",
                scopes: commonScopes.Union(new[]
                {
                    "IdentityService"
                }),
                grantTypes: new[] { "client_credentials" },
                secret: "www.guili.co".Sha256(),
                permissions: new[] { IdentityPermissions.Users.Default }
            );
        }

        private async Task<Client> CreateClientAsync(
            string name,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret = null,
            List<string> redirectUris = null,
            string postLogoutRedirectUri = null,
            string frontChannelLogoutUri = null,
            bool requireClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> permissions = null,
            IEnumerable<string> corsOrigins = null)
        {
            var client = await _clientRepository.FindByClientIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        FrontChannelLogoutUri = frontChannelLogoutUri,
                        RequireClientSecret = requireClientSecret,
                        RequirePkce = requirePkce
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            if (redirectUris != null)
            {
                foreach (var redirectUri in redirectUris)
                {
                    if (redirectUri != null)
                    {
                        if (client.FindRedirectUri(redirectUri) == null)
                        {
                            client.AddRedirectUri(redirectUri);
                        }
                    }
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions,
                    null
                );
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }
}
