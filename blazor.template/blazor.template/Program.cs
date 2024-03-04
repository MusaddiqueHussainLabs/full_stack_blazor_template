using blazor.template.client.infrastructure.Authentication;
using blazor.template.client.infrastructure.Managers.Identity.Authentication;
using blazor.template.client.infrastructure.Managers.Interceptors;
using blazor.template.client.infrastructure.Managers.Preferences;
using blazor.template.client.Pages;
using blazor.template.Components;
using blazor.template.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

string ClientName = "BlazorHero.API";

builder.Services.AddRazorPages();

builder.Services.AddLocalization(options =>
                {
                    options.ResourcesPath = "Resources";
                })
                .AddCascadingAuthenticationState()
                .AddBlazoredLocalStorage()
                .AddMudServices()
                .AddScoped<ClientPreferenceManager>()
                .AddScoped<BlazorStateProvider>()
                .AddScoped<AuthenticationStateProvider, BlazorStateProvider>()
                .AddManagers()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp));

builder.Services.AddHttpClient<IAuthenticationManager, AuthenticationManager>();
builder.Services.AddHttpClientInterceptor();
//builder.Services.AddTransient<IHttpInterceptorManager, HttpInterceptorManager>();
//builder.Services.AddTransient<IClientPreferenceManager, ClientPreferenceManager>();

//builder.Services.AddManagers();
//builder.Services.AddScoped(sp => sp
//                    .GetRequiredService<IHttpClientFactory>()
//                    .CreateClient(ClientName).EnableIntercept(sp));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(blazor.template.client._Imports).Assembly);

app.Run();
