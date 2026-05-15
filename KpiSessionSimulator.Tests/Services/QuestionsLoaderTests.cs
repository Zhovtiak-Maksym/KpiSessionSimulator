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
            // Act
            var questions = await QuestionsLoader.LoadQuestionsAsync(PathsMacker.OpQuestions);

            // Assert
            Assert.That(questions, Is.Not.Null);
            Assert.That(questions.Count, Is.GreaterThan(0));
        }

    }
}