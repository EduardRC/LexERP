using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LexERP.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient _httpClient;

        public Repositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(httpResponse, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, httpResponse);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, httpResponse);
            }
        }


        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PostAsync(url, enviarContent);

            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PostAsync(url, enviarContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(httpResponse, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, httpResponse);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, httpResponse);
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PutAsync(url, enviarContent);

            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var httpResponse = await _httpClient.DeleteAsync(url);

            return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
        }


        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }
    }
}
