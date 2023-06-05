using MudBlazor;
using PodTube.Shared.Models.RequestBody;

namespace PodTube.Client.Rest {
    public class RestHelper {
        private ISnackbar _snackbar;
        public RestHelper(ISnackbar snackbar) {
            _snackbar = snackbar;
        }

        public async Task MakeSafeRestCall(Func<Task> action, string OnSuccess = "") {
            try {
                await action();
                if(OnSuccess.Length > 0) {
                    _snackbar.Add(OnSuccess, Severity.Success);
                }
            } catch (ApiException e) {
                string message;
                if (e.StatusCode == 401) {
                    message = "Please login!";
                } else {
                    message = e.Response != null ? e.Response : e.Message;
                }
                _snackbar.Add(message, Severity.Error);
            }
        }
    }
}
