using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Tests.Teachers
{
    [TestFixture]
    public class OpTeacherTests
    {
        [Test]
        public void Interact_ShouldGiveFreeCorrectAnswer_WhenPlayerHasDeaths()
        {
            // Arrange
            var teacher = new OpTeacher();
            var player = new Player();
            player.Stats = new PlayerStats { Deaths = 1 };
            var state = new ExamState();

            // Act
            teacher.Interact(player, state);

            // Assert
            Assert.That(state.CorrectAnswers, Is.EqualTo(1));
        }
    }
}