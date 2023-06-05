using Blazored.LocalStorage;

namespace PodTube.Client.Services {
    public class TokenService {
        private static readonly string JWT_TOKEN_KEY = "token";

        private ILocalStorageService _localStorage;

        public TokenService(ILocalStorageService localStorage) {
            _localStorage = localStorage;
            
        }
        private string authToken = "";

        public bool isTokenValid {
            get {
                return authToken.Length > 0;
            }
        }
        
        public async Task<string> GetToken() {
            if(!isTokenValid) {
                await LoadTokenFromPersistentStorage();
            }
            return authToken;
        }

        public async Task UpdateToken(string token) {
            authToken = token;
            await _localStorage.SetItemAsync<string>(JWT_TOKEN_KEY, authToken);
        }

        public async Task LoadTokenFromPersistentStorage() {
            authToken = await _localStorage.GetItemAsync<string>(JWT_TOKEN_KEY) ?? "";
        }
    }
}
