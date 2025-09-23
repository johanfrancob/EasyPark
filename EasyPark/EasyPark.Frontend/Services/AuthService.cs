using EasyPark.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace EasyPark.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(HttpClient http, IJSRuntime js, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _js = js;
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
        }

        public async Task<bool> LoginAsync(string usuario, string contrasena)
        {
            var response = await _http.PostAsJsonAsync("api/Login", new LoginRequest
            {
                Nombre = usuario,
                Contrasena = contrasena
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var loginData = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginData is null || string.IsNullOrEmpty(loginData.Token))
                return false;

            await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", loginData.Token);


            _authStateProvider.NotifyUserAuthentication(loginData.Token);

            return true;
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _authStateProvider.NotifyUserLogout();
        }
    }
}
