using EasyPark.Frontend;
using EasyPark.Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddTransient<AuthHeaderHandler>();
object value = builder.Services.AddHttpClient("EasyParkApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7202/");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EasyParkApi"));

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7202/") });

await builder.Build().RunAsync();

