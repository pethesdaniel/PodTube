using Radzen;
namespace PodTube.Client.Editor.Util {
    public static class ServiceCollectionExtensions {
        public static void AddEditorServices(this IServiceCollection services) {
            services.AddRadzenComponents();
        }
    }
}
