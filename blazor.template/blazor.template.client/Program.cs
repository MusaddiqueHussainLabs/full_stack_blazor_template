using blazor.template.client.Extensions;
using blazor.template.client.infrastructure.Managers.Preferences;
using blazor.template.client.infrastructure.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using shared.Constants.Localization;
using System.Globalization;

var builder = WebAssemblyHostBuilder
                .CreateDefault(args)
                .AddClientServices();

var host = builder.Build();
var storageService = host.Services.GetRequiredService<ClientPreferenceManager>();
if (storageService != null)
{
    CultureInfo culture;
    var preference = await storageService.GetPreference() as ClientPreference;
    if (preference != null)
        culture = new CultureInfo(preference.LanguageCode);
    else
        culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await builder.Build().RunAsync();
