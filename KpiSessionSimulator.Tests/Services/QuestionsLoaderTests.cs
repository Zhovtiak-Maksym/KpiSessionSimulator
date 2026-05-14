using KpiSessionSimulator.Services;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class QuestionsLoaderTests
    {
        [Test]
        public void LoadQuestionsAsync_ShouldThrowExceptionWithFileNotFound()
        {
            // Arrange
            string fakePath = "smth_fake.json";

            // Act & Assert
            Assert.ThrowsAsync<FileNotFoundException>(async () =>
            {
                await QuestionsLoader.LoadQuestionsAsync(fakePath);
            });
        }

        [Test]
        public async Task LoadQuestionsAsync_ShouldReturnQuestionsList_WhenFileExists()
        {
            // Arrange
            string baseDir = AppContext.BaseDirectory;
            string path = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Data", "op_questions.json"));

            // Act
            var questions = await QuestionsLoader.LoadQuestionsAsync(path);

            // Assert
            Assert.That(questions, Is.Not.Null);
            Assert.That(questions.Count, Is.GreaterThan(0));
            Assert.That(questions[0].Text, Is.Not.Null.And.Not.Empty);
            Assert.That(questions[0].Options.Count, Is.GreaterThan(0));
        }
    }
}