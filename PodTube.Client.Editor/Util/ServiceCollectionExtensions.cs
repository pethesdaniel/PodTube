using Microsoft.Extensions.DependencyInjection;
using PodTube.Client.Editor.Services;
using Radzen;
namespace PodTube.Client.Editor.Util {
    public static class ServiceCollectionExtensions {
        public static void AddEditorServices(this IServiceCollection services) {
            services.AddSingleton<TimelineService>();
            services.AddRadzenComponents();
        }
    }
}
