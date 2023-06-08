using Microsoft.AspNetCore.Components;
using PodTube.Client.Rest;
using PodTube.Shared.Models.DTO;
using System.Net.Http;

namespace PodTube.Client.Services {
    public class LoginService {
        private TokenService _tokenService;
        private HttpClient _httpClient;
        private AuthApiClient client;

        public LoginService(TokenService tokenService, HttpClient httpClient) {
            _httpClient = httpClient;
            _tokenService = tokenService;
            client = new AuthApiClient(_httpClient.BaseAddress?.AbsolutePath ?? "", _httpClient);
        }

        public bool IsLoggedIn {
            get {
                return _tokenService.isTokenValid;
            }
        }

        public async Task Login(string email, string password) {
            var result = await client.LoginAsync(new PodTube.Shared.Models.RequestBody.AuthRequestBody {
                Email = email,
                Password = password
            });
            if(result != null && result.Token != null) {
                await _tokenService.UpdateToken(result.Token);
            }
        }

        public async Task Logout() {
            await _tokenService.UpdateToken("");
        }
        
        public async Task TryAutoLogin() {
            await _tokenService.LoadTokenFromPersistentStorage();
            try {
                await client.VerifyAsync();
            } catch (ApiException e) {
                if(e.StatusCode == 401) {
                    await Logout();
                } else {
                    throw e;
                }
            }
        }
    }
}
