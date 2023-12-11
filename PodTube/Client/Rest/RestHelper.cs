using MudBlazor;
using PodTube.Shared.Models.RequestBody;

namespace PodTube.Client.Rest {
    public class RestHelper {
        private ISnackbar _snackbar;

        private static readonly object Void = new object();
        public RestHelper(ISnackbar snackbar) {
            _snackbar = snackbar;
        }

        public async Task MakeSafeRestCall(Func<Task> action, string OnSuccess = "") {
            await MakeSafeRestCall<object>(async () => { await action(); return Void; }, OnSuccess);
        }


        public async Task<T?> MakeSafeRestCall<T>(Func<Task<T>> action, string OnSuccess = "") {
            try {
                T? result = await action();
                if (OnSuccess.Length > 0) {
                    _snackbar.Add(OnSuccess, Severity.Success);
                }
                return result;
            } catch (ApiException e) {
                string message;
                if (e.StatusCode == 401) {
                    message = "Please login!";
                } else {
                    message = e.Response != null ? e.Response : e.Message;
                }
                _snackbar.Add(message, Severity.Error);
                return default(T);
            }
        }
    }
}
