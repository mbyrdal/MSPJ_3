using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ClientWeb.Utilities
{
    public static class SessionHelper
    {
        // Set an object as JSON in the session
        public static void SetObjectAsJSON(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Get an object from JSON in the session
        public static T GetObjectFromJSON<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
