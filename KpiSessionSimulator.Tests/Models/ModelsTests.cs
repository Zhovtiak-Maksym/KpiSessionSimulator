using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Tests.Models
{
    [TestFixture]
    public class ModelsTests
    {
        [Test]
        public void PlayerConstructor_ShouldSetDefaultValues()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.That(player.Faculty, Is.EqualTo("FPSPM"));
            Assert.That(player.WrongAnswersStreak, Is.EqualTo(0));
            Assert.That(player.Stats.Tokens, Is.EqualTo(30));
            Assert.That(player.Stats.IsExpelled, Is.False);
            Assert.That(player.Stats.IsONSecondary, Is.False);
            Assert.That(player.Stats.PassedExams, Is.EqualTo(0));
            Assert.That(player.Stats.Deaths, Is.EqualTo(0));
            Assert.That(player.Stats.EagleEyeCount, Is.EqualTo(0));
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(0));
            Assert.That(player.Stats.LoyaltyCount, Is.EqualTo(0));
            Assert.That(player.Stats.TrickyHandsCount, Is.EqualTo(0));
        }

        [Test]
        public void ExamStateConstructor_ShouldSetDefaultValues()
        {
            // Arrange & Act
            var state = new ExamState();

            // Assert
            Assert.That(state.CorrectAnswers, Is.EqualTo(0));
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.Easy));
            Assert.That(state.BlackjackLosses, Is.EqualTo(0));
            Assert.That(state.IsHospitalized, Is.False);
        }

        [Test]
        public void QuestionConstructor_ShouldInitializeListAndDefaultDifficulty()
        {
            // Arrange & Act
            var question = new Question();

            // Assert
            Assert.That(question.Options, Is.Not.Null);
            Assert.That(question.Difficulty, Is.EqualTo(Difficulty.Easy));
        }
    }
}
