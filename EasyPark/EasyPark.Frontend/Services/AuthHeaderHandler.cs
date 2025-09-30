using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace EasyPark.Frontend.Services 
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;

        public AuthHeaderHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Leemos el token guardado en sessionStorage con la clave "authToken"
            var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                // Si el token existe, lo añadimos a la cabecera de la petición
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Enviamos la petición ya modificada al servidor
            return await base.SendAsync(request, cancellationToken);
        }
    }
}