using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TutorHub.Web;
using TutorHub.Web.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
var config = builder.Configuration;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<TokenService>();
builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IFlowbiteService, FlowbiteService>();
builder.Services.AddHttpClient<AuthClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5089/api/");
});

builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5089/api/");
})
.AddHttpMessageHandler<AuthTokenHandler>() // <-- ADD YOUR HANDLER HERE
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true 
});



await builder.Build().RunAsync();
