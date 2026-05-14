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
    }
}