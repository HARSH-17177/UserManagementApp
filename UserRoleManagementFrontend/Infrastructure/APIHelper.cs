using System.Text;
using System.Text.Json;

namespace UserRoleManagementFrontend.Infrastructure
{
    public static class ApiHelper
    {
        public static async Task<T> ExecuteHttpGet<T>(string url, string token, string baseUrl) where T : class
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }

        public static async Task<T> ExecuteHttpDelete<T>(string url, string token, string baseUrl) where T : class
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }


        public static async Task<T> ExecuteHttpPut<T>(string url, string token, string baseUrl, object data) where T : class
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }


        public static async Task<T> ExecuteHttpPost<T>(string url, string token, string baseUrl, object data) where T : class
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>();
                return result!;
            }

            return null; 
        }




    }
}
