using blazor.template.client.infrastructure.Settings;
using MudBlazor;

namespace blazor.template.Components.Layout
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme _currentTheme;
        private bool _rightToLeft = false;
        private async Task RightToLeftToggle(bool value)
        {
            _rightToLeft = value;
            await Task.CompletedTask;
        }

        //protected override async Task OnInitializedAsync()
        //{
        //    _currentTheme = BlazorTheme.DefaultTheme;
        //    _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
        //    _rightToLeft = await _clientPreferenceManager.IsRTL();
        //    _interceptor.RegisterEvent();
        //}

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _currentTheme = BlazorTheme.DefaultTheme;
                _currentTheme = await _clientPreferenceManager.GetCurrentThemeAsync();
                _rightToLeft = await _clientPreferenceManager.IsRTL();
                _interceptor.RegisterEvent();

                StateHasChanged();
            }
        }

        private async Task DarkMode()
        {
            bool isDarkMode = await _clientPreferenceManager.ToggleDarkModeAsync();
            _currentTheme = isDarkMode
                ? BlazorTheme.DefaultTheme
                : BlazorTheme.DarkTheme;
        }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}
