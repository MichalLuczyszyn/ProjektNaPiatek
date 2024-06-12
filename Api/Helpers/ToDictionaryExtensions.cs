using System.Reflection;

namespace Api.Helpers;

public class ToDictionaryExtensions
{
    public static Dictionary<string, string> ToDictionary(object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var dict = new Dictionary<string, string>();
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            dict[property.Name] = value != null ? value.ToString() : string.Empty;
        }

        return dict;
    }
}