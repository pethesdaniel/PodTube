using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Newtonsoft.Json.Linq;
using PodTube.Client;
using PodTube.Client.Rest;
using PodTube.Client.Rest.Authorization;
using PodTube.Client.Services;
using System.Net.Http;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TokenHandler>();
builder.Services.AddScoped<RestHelper>();

builder.Services.AddScoped(
    (sp) => {
        var handler = sp.GetRequiredService<TokenHandler>();
        return new HttpClient(handler) {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
        };
    }
    


);

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
