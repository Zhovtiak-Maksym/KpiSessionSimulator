using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;

namespace KpiSessionSimulator.Tests
{
    [TestFixture]
    public class StreakAndDifficultyTests
    {
        private Player _player;
        private ExamState _state;
        private DifficultyManager _diffManager;

        [SetUp]
        public void Setup()
        {
            _player = new Player
            {
                NickName = "Smn",
                WrongAnswersStreak = 0 
            };

            _state = new ExamState
            {
                CurrentDifficulty = Difficulty.Easy 
            };

            _diffManager = new DifficultyManager();
        }

        [Test]
        public void WrongAnswersStreak_ShouldTriggerDifficultyAndIncreaseToNormal()
        {
            // Arrange 
            _player.WrongAnswersStreak = 5; 

            // Act 
            bool shouldIncrease = _player.WrongAnswersStreak > 0 && _player.WrongAnswersStreak % 5 == 0;

            if (shouldIncrease)
            {
                _diffManager.IncreaseDifficulty(_state);
            }

            // Assert 
            Assert.That(shouldIncrease, Is.True);
            Assert.That(_state.CurrentDifficulty, Is.EqualTo(Difficulty.Normal));
        }

        [Test]
        public void WrongAnswersStreak_ShouldTriggerDifficultyAndIncreaseToDifficult()
        {
            // Arrange 
            _player.WrongAnswersStreak = 10;
            _state.CurrentDifficulty = Difficulty.Medium;

            // Act
            bool shouldIncrease = _player.WrongAnswersStreak > 0 && _player.WrongAnswersStreak % 5 == 0;

            if (shouldIncrease)
            {
                _diffManager.IncreaseDifficulty(_state);
            }

            // Assert 
            Assert.That(shouldIncrease, Is.True);
            Assert.That(_state.CurrentDifficulty, Is.EqualTo(Difficulty.Difficult));
        }

        [Test]
        public void WrongAnswersStreak_ShouldNotTriggerDifficultyAndIncrease()
        {
            // Arrange
            _player.WrongAnswersStreak = 4; 

            // Act
            bool shouldIncrease = _player.WrongAnswersStreak > 0 && _player.WrongAnswersStreak % 5 == 0;

            if (shouldIncrease)
            {
                _diffManager.IncreaseDifficulty(_state);
            }

            // Assert
            Assert.That(shouldIncrease, Is.False);
            Assert.That(_state.CurrentDifficulty, Is.EqualTo(Difficulty.Easy)); 
        }

        [Test]
        public void MinigameWin_ShouldRollbackStreak()
        {
            // Arrange
            _player.WrongAnswersStreak = 3;
            bool wonMinigame = true; 

            // Act 
            if (wonMinigame && _player.WrongAnswersStreak > 0)
            {
                _player.WrongAnswersStreak--;
            }

            // Assert
            Assert.That(_player.WrongAnswersStreak, Is.EqualTo(2));
        }

        [Test]
        public void MinigameWin_ShouldNotRollbackStreak()
        {
            // Arrange
            _player.WrongAnswersStreak = 0; 
            bool wonMinigame = true;

            // Act
            if (wonMinigame && _player.WrongAnswersStreak > 0)
            {
                _player.WrongAnswersStreak--;
            }

            // Assert
            Assert.That(_player.WrongAnswersStreak, Is.EqualTo(0));
        }
    }
}
