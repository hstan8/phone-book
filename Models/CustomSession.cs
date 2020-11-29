using System;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace phone_book.Models
{
    public static class CustomSession
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string str, string value, StringComparison comparison)
        {
            return str.IndexOf(value, comparison) >= 0;
        }
    }
}
