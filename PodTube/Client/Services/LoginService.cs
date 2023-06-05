using Microsoft.AspNetCore.Components;
using PodTube.Client.Rest;
using PodTube.Shared.Models.DTO;
using System.Net.Http;

namespace PodTube.Client.Services {
    public class LoginService {
        private TokenService _tokenService;
        private HttpClient _httpClient;

        public LoginService(TokenService tokenService, HttpClient httpClient) {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public bool IsLoggedIn {
            get {
                return _tokenService.isTokenValid;
            }
        }

        public async Task Login(string email, string password) {
            var authApi = new AuthApiClient(_httpClient.BaseAddress?.ToString() ?? "", _httpClient);
            var result = await authApi.LoginAsync(new PodTube.Shared.Models.RequestBody.AuthRequestBody {
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
    }
}
