using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TutorHub.Web;
using TutorHub.Web.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
var config = builder.Configuration;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IFlowbiteService, FlowbiteService>();
builder.Services.AddScoped<AuthClient>(sp =>
{
    var http = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5089/api/")
    };
    return new AuthClient(http);
});




await builder.Build().RunAsync();
