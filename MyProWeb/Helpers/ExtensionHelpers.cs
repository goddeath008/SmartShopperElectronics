using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace MyProWeb.Helpers
{
    public static class ExtensionHelpers
    {
        public static string ToVND(this int GiaTri)
        {
            return $"{GiaTri:#,##0.00} vnđ";
        }

         public static void Set<T>(this ISession session, string key, T value)
            {
                session.SetString(key, JsonSerializer.Serialize(value));
            }

         public static T? Get<T>(this ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default : JsonSerializer.Deserialize<T>(value);
            }
        
    }
}
