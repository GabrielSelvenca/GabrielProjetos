using System.Text;
using System.Text.Json;

namespace WS_Tower.Service
{
    public static class ApiService<T> where T : class
    {
        private static HttpClient ?client;

        private static HttpClient Client
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
            return json!;
        }

        public static async Task<List<T>> GetList(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<List<T>>(content);
            return json!;
        }

        public static async Task<T> Post(string url, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error {response.StatusCode}: {responseBody}");

            if (responseBody == null)
                return null;

            var result = JsonSerializer.Deserialize<T>(responseBody);
            return result!;
        }

        public static async Task<T> Put(string url, T obj)
        {
            var json = JsonSerializer.Serialize<T>(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error {response.StatusCode}: {responseBody}");

            if (responseBody == null)
                return null;

            var result = JsonSerializer.Deserialize<T>(responseBody);
            return result!;
        }

        public static async Task<T> Delete(string url, int id)
        {
            var response = await Client.DeleteAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<T>(content);
            return json!;
        }
    }
}
