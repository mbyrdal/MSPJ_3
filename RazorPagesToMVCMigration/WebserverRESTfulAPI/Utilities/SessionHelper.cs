using System.Text.Json;

namespace ServiceAPI.Utilities
{
    public static class SessionHelper
    {
        public static void SetObjectAsJSON(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJSON<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
