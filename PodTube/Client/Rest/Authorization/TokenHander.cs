using PodTube.Client.Services;
using System.Net;
using System.Net.Http.Headers;

namespace PodTube.Client.Rest.Authorization {
    public class TokenHandler : DelegatingHandler {
        private TokenService _tokenService;

        public TokenHandler(TokenService tokenService) : this(tokenService, null) {}

        public TokenHandler(TokenService tokenService, HttpMessageHandler innerHandler) {
            _tokenService = tokenService;
            InnerHandler = innerHandler ?? new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request, CancellationToken cancellationToken) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized) {
                await _tokenService.UpdateToken("");
            };
            return result;
        }
    }
}
