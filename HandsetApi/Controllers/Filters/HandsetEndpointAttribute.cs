using Roi.Analysis.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Roi.Analysis.Api.Filters
{
    public class HandsetEndpointAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => true;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.Request.Headers.Authorization == null ||
                string.IsNullOrEmpty(context.Request.Headers.Authorization.Parameter))
                
            {
                context.ErrorResult = new AuthenticationFailureResult("Authentication Required", context.Request);
                context.ActionContext.Response.StatusCode = HttpStatusCode.Forbidden;
            }
            else
            {
                context.Principal = new HandsetPrincipal(context.Request.Headers.Authorization.Parameter);
              
            }
        }

        private Task ChallengeAsync(HttpAuthenticationContext context, CancellationToken cancellationToken, HttpRequestMessage request)
        {
            return Task.Run(() => { ChallengeAsync(new HttpAuthenticationChallengeContext(context.ActionContext, context.ErrorResult), cancellationToken); });
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.Run(() => context);
        }

        private class AuthenticationFailureResult : IHttpActionResult
        {
            private HttpRequestMessage request;
            private HttpResponseMessage response;

            public AuthenticationFailureResult(string v, HttpRequestMessage request)
            {
                this.request = request;
                response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.Forbidden, ReasonPhrase = v };
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.Run(() => response);
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

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return InnerResult.ExecuteAsync(cancellationToken).ContinueWith(t =>
                {
                    try
                    {
                        t.Wait();
                    }
                    catch (Exception)
                    {
                        if (!t.IsCompleted) return null;
                    }
                    var r = t.IsCanceled?null: t.Result;
                    if (r == null) return null;
                    if (r.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // Only add one challenge per authentication scheme.
                        if (!r.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                        {
                            r.Headers.WwwAuthenticate.Add(Challenge);
                        }
                    }
                    return r;
                });
            }
        }
    }
}