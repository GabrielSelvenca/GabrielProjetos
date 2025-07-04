using System.Text.Json;
using System.Text;
using System.Diagnostics;
using ConsumoAPIMaui.Models;

namespace ConsumoAPIMaui.Services
{
    public static class ApiService<T> where T : class
    {
        private static HttpClient client;

        public static HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://10.0.2.2:5000/api/");
                }
                return client;
            }
        }

        public static async Task<T> Get(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<T>(content);
            return json;
        }

        public static async Task<List<T>> GetList(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<List<T>>(content);
            return json;
        }

        public static async Task<T> Post(string url, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseRequest = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseRequest);

            return result;
        }

        public static async Task<T> Put(string url, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseRequest = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseRequest);

            return result;
        }

        public static async Task<T> Patch(string url, T obj, int id)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PatchAsync($"{url}/{id}", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro {response.StatusCode}: {responseBody}");
            }

            if (string.IsNullOrWhiteSpace(responseBody))
                return null;

            var result = JsonSerializer.Deserialize<T>(responseBody);
            return result;
        }
    }
}
