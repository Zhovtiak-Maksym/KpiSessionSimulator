using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class DifficultyManagerTests
    {
        [Test]
        public void IncreaseDifficulty_FromEasyToNormal()
        {
            // Arrange
            var state = new ExamState { CurrentDifficulty = Difficulty.Easy };
            var manager = new DifficultyManager();

            // Act
            manager.IncreaseDifficulty(state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.Normal));
        }

        [Test]
        public void IncreaseDifficulty_FromNormalToMedium()
        {
            // Arrange
            var state = new ExamState { CurrentDifficulty = Difficulty.Normal };
            var manager = new DifficultyManager();

            // Act
            manager.IncreaseDifficulty(state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.Medium));
        }

        [Test]
        public void IncreaseDifficulty_FromMediumToDifficult()
        {
            // Arrange
            var state = new ExamState { CurrentDifficulty = Difficulty.Medium };
            var manager = new DifficultyManager();

            // Act
            manager.IncreaseDifficulty(state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.Difficult));
        }

        [Test]
        public void IncreaseDifficulty_FromDifficultToDeathMode()
        {
            // Arrange
            var state = new ExamState { CurrentDifficulty = Difficulty.Difficult };
            var manager = new DifficultyManager();

            // Act
            manager.IncreaseDifficulty(state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.DeathMode));
        }

        [Test]
        public void IncreaseDifficulty_ShouldStayDeathMode()
        {
            // Arrange
            var state = new ExamState { CurrentDifficulty = Difficulty.DeathMode };
            var manager = new DifficultyManager();

            // Act
            manager.IncreaseDifficulty(state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.DeathMode));
        }
    }
}