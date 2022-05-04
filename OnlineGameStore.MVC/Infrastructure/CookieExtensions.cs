using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class CookieExtensions
    {
        public static void SetJson(this IResponseCookies responseCookie, string key, object value, CookieOptions options)
        {
            var serializeObject = JsonConvert.SerializeObject(value,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            responseCookie.Append(key, serializeObject, options);
        }

        public static T GetJson<T>(this IRequestCookieCollection requestCookie, string key)
        {
            var cookieData = requestCookie[key];

            var deserializeObject = cookieData == null
                ? default
                : JsonConvert.DeserializeObject<T>(cookieData);

            return deserializeObject;
        }
    }
}
