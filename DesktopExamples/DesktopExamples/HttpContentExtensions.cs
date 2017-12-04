using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Net.Http
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }
    }
}
