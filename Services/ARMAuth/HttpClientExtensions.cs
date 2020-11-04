using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.RequestEcho
{
    public static class HttpClientExtensions
    {
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string requestUri)
        {
            string objectInString = await httpClient.GetStringAsync(requestUri).ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(objectInString, _serializerOptions);
        }
    }
}