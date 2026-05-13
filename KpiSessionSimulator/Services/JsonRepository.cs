using System.Text.Json;
using System.Text.Json.Serialization;

namespace KpiSessionSimulator.Services
{
    public static class JsonRepository<T> where T : new()
    {
        public static async Task<T> LoadAsync(string path)
        {
            if (!File.Exists(path))
            {
                return new T();
            }

            string jsonStr = await File.ReadAllTextAsync(path);

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(jsonStr, options) ?? new T();
        }

        public static async Task SaveAsync(string path, T data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonStr = JsonSerializer.Serialize(data, options);

            await File.WriteAllTextAsync(path, jsonStr);
        }
    }
}