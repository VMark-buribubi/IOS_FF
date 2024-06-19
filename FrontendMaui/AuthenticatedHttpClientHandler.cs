using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authToken = await SecureStorage.GetAsync("auth_token");
            if (!string.IsNullOrEmpty(authToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
