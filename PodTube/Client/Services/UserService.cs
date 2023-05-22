using Blazored.SessionStorage;
using PodTube.Client.Rest;
using PodTube.Shared.Models.DTO;
using System.Net.Http;

namespace PodTube.Client.Services {
    public class UserService {
        private static readonly string JWT_TOKEN_KEY = "token";

        private ISessionStorageService _sessionManager;
        private HttpClient _httpClient;

        public UserService(ISessionStorageService sessionStorage, HttpClient httpClient) {
            _sessionManager = sessionStorage;
            _httpClient = httpClient;
        }
        private UserDto? _user = null;

        public async Task<UserDto?> GetUser() {
            if (_user == null) {
                var token = await _sessionManager.GetItemAsync<string>(JWT_TOKEN_KEY);
                if(token == null) {
                    return null;
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var userApi = new UserApiClient(_httpClient.BaseAddress?.ToString() ?? "", _httpClient);
                _user = await userApi.GetUserSelfAsync();
            }
            return _user;
        }

        public async Task<AuthResponseDTO> Login(string email, string password) {
            var authApi = new AuthApiClient(_httpClient.BaseAddress?.ToString() ?? "", _httpClient);
            var result = await authApi.LoginAsync(new PodTube.Shared.Models.RequestBody.AuthRequestBody {
                Email = email,
                Password = password
            });
            if(result != null && result.Token != null) {
                await _sessionManager.SetItemAsync(JWT_TOKEN_KEY, result.Token);
            }
            return result ?? new AuthResponseDTO();
        }
    }
}
