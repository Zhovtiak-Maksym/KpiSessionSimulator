using KpiSessionSimulator.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KpiSessionSimulator.Services
{
    public class QuestionsLoader
    {
        public static List<Question> LoadQuestions(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Шлях до бази даних питань не знайдено: {path}");
            }

            string jsonStr = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Question>>(jsonStr, options) ?? new List<Question>();
        }
    }
}
