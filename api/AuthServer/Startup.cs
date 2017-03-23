using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using AuthServer.App_Start;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Owin.Security.Infrastructure;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.Common;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Owin.Security;
using Ninject;

[assembly: OwinStartup(typeof(AuthServer.ResourceServer.Startup))]
namespace AuthServer.ResourceServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Setup Authorization Server
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = new PathString("/auth"),
                TokenEndpointPath = new PathString("/token"),
                ApplicationCanDisplayErrors = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                AllowInsecureHttp = true,
                // Authorization server provider which controls the lifecycle of Authorization Server
                Provider = new OAuthAuthorizationServerProvider
                {
                    //OnValidateClientRedirectUri = ValidateClientRedirectUri,
                    OnValidateClientAuthentication = ValidateClientAuthentication,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                    //OnGrantClientCredentials = GrantClientCredetails,
                    OnGrantRefreshToken = GrantRefreshToken
                },

                // Authorization code provider which creates and receives the authorization code.
                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode,
                },

                // Refresh token provider which creates and receives refresh token.
                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken,
                    OnCreateAsync = CreateRefreshTokenAsync,
                    OnReceiveAsync = ReceiveRefreshTokenAsync
                }
            });
        }

        private Task CreateRefreshTokenAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenLifeTimeMinutes = 3;
            var tokenBody = PasswordHelper.CreateShaHash(context.Ticket.Identity.Name + Guid.NewGuid());
            var userId = new Guid(context.Ticket.Properties.Dictionary["UserId"]);

            var issuedTokenUtc = DateTime.UtcNow;
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = PasswordHelper.CreateShaHash(tokenBody),
                IssuedUtc = issuedTokenUtc,
                ExpiresUtc = issuedTokenUtc.AddMinutes(refreshTokenLifeTimeMinutes)
            };

            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;
            refreshToken.ProtectedTicket = context.SerializeTicket();

            var refreshTokenService = NinjectWebCommon.Bootstrapper.Kernel.Get<IRefreshTokenService>();
            refreshTokenService.Add(refreshToken);
            context.SetToken(tokenBody);

            return Task.FromResult<object>(null);
        }

        private Task ReceiveRefreshTokenAsync(AuthenticationTokenReceiveContext context)
        {
            var hashedRefreshToken = PasswordHelper.CreateShaHash(context.Token);
            var refreshTokenService = NinjectWebCommon.Bootstrapper.Kernel.Get<IRefreshTokenService>();
            var existingToken = refreshTokenService.GetByToken(hashedRefreshToken);
            if (existingToken != null)
            {
                context.DeserializeTicket(existingToken.ProtectedTicket);
                refreshTokenService.Delete(existingToken);
            }

            return Task.FromResult<object>(null);
        }

        private Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var reNewIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var reNewTicket = new AuthenticationTicket(reNewIdentity, context.Ticket.Properties);
            context.Validated(reNewTicket);

            return Task.FromResult<object>(null);
        }

        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userService = NinjectWebCommon.Bootstrapper.Kernel.Get<IUserService>();
            var currentUser = userService.GetByCredentials(context.UserName, context.Password);
            if (currentUser == null)
            {
                context.SetError("The user name or password is incorrect.");
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
            var properties = new AuthenticationProperties(GetAuthenticatedProperties(currentUser));
            var ticket = new AuthenticationTicket(identity,properties);
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        private void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authenticationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(
                context.ClientId, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x))
                );

            context.Validated(identity);

            return Task.FromResult(0);
        }

        private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            //context.SetToken(context.SerializeTicket());
            throw new NotImplementedException();
        }

        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            // context.DeserializeTicket(context.Token);
            throw new NotImplementedException();

        }

        private Dictionary<string, string> GetAuthenticatedProperties(User user)
        {
            var result = new Dictionary<string, string>
            {
                { "UserId", user.Id.ToString() },
                { "UserName", $"{user.FirstName}{user.LastName}"}
            };

            return result;
        }
    }
}
