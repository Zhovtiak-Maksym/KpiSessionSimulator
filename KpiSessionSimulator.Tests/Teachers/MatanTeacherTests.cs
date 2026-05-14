using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Tests.Teachers
{
    [TestFixture]
    public class MatanTeacherTests
    {
        [Test]
        public void Interact_ShouldDoNothing_WhenPlayerIsExpelled()
        {
            // Arrange
            var teacher = new MatanTeacher();
            var player = new Player();
            player.Stats = new PlayerStats { IsExpelled = true };
            var state = new ExamState();

            // Act
            teacher.Interact(player, state);

            // Assert
            Assert.That(state.CurrentDifficulty, Is.EqualTo(Difficulty.Easy));
        }
    }
}