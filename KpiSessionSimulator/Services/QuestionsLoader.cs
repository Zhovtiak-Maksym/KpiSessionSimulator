using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Services
{
    public class QuestionsLoader
    {
        public static async Task<List<Question>> LoadQuestionsAsync(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Path to the questions database not found: {path}");
            }

            return await JsonRepository<List<Question>>.LoadAsync(path);
        }
    }
}
