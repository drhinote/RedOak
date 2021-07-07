using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Linq;
using System.Text;

namespace OData_Server
{
    public class JwtAuthAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => true;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            System.Net.Http.Headers.AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(context.Request.Headers.Authorization.Parameter))

            {
                context.ActionContext.Response.StatusCode = HttpStatusCode.Forbidden;
                context.ErrorResult = new AuthenticationFailureResult("Authentication Required", context.Request);
                return;
            }

            if (context.Request.Headers.Authorization.Scheme == "Basic")
            {
                context.Principal = AuthenticateJwtToken(Encoding.UTF8.GetString(Convert.FromBase64String(context.Request.Headers.Authorization.Parameter)).Split(':')[0]);
            }
            else
            {
                context.Principal = AuthenticateJwtToken(context.Request.Headers.Authorization.Parameter);
            }
            if(context.Principal == null)
            {
                context.ActionContext.Response.StatusCode = HttpStatusCode.Forbidden;
                context.ErrorResult = new AuthenticationFailureResult("Authentication Required", context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Bearer");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }

        //aCob14AVV0pBN2KxIzadHOr1YGdVU2zwztulCedA8U0bAtNREq7cM4HlHiMiAJJc
        private const string Secret = "YUNvYjE0QVZWMHBCTjJLeEl6YWRIT3IxWUdkVlUyend6dHVsQ2VkQThVMGJBdE5SRXE3Y000SGxIaU1pQUpKYw==";

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                
                return principal;
            }
            catch
            {

                return null;
            }
        }

        private static bool ValidateToken(string token, out List<Claim> claims)
        {
            claims = new List<Claim>();

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            claims.AddRange(identity.Claims.Where(c => c.Type == ClaimTypes.GroupSid));

            if (!claims.Any())
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            if(usernameClaim == null) return false;
            claims.Add(usernameClaim);

            return true;
        }

        protected IPrincipal AuthenticateJwtToken(string token)
        {

            if (ValidateToken(token, out List<Claim> claims))
            {
                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);
                return user;
            }

            return null;
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }

    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            Challenge = challenge;
            InnerResult = innerResult;
        }

        public AuthenticationHeaderValue Challenge { get; private set; }

        public IHttpActionResult InnerResult { get; private set; }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Only add one challenge per authentication scheme.
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(Challenge);
                }
            }

            return response;
        }
    }
}