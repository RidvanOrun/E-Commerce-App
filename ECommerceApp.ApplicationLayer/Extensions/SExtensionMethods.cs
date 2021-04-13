using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace ECommerceApp.ApplicationLayer.Extensions
{
    public static class SExtensionMethods
    {
        public static void SetObject<T>(this ISession session, string key, T value) => session.SetString(key, JsonConvert.SerializeObject(value));

        public static T GetObject<T>(this ISession session, string key) => session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));

    }
}
