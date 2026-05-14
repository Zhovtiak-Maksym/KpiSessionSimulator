using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class DatabaseTests
    {
        private const string TestFilePath = "test_profiles.json";

        [Test]
        public async Task JsonRepository_ShouldSaveAndLoadDataCorrectly()
        {
            // Arrange
            var dataToSave = new List<Player>
            {
                new Player { NickName = "Smn", Password = "12345" },
                new Player { NickName = "Smn2", Password = "22222" }
            };

            // Act
            await JsonRepository<List<Player>>.SaveAsync(TestFilePath, dataToSave);
            var loadedData = await JsonRepository<List<Player>>.LoadAsync(TestFilePath);

            // Assert
            Assert.That(loadedData, Is.Not.Null);
            Assert.That(loadedData.Count, Is.EqualTo(2));
            Assert.That(loadedData[0].NickName, Is.EqualTo("Smn"));
            Assert.That(loadedData[1].NickName, Is.EqualTo("Smn2"));

            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }
    }
}