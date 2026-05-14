using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;

namespace KpiSessionSimulator.Tests.Punishments
{
    [TestFixture]
    public class TransferToFiotPenaltyTests
    {
        [Test]
        public void DoPunishment_ShouldChangeFaculty()
        {
            // Arrange
            var player = new Player();
            player.Faculty = "FPSPM";
            var penalty = new TransferToFiotPenalty("FIOT");

            // Act
            penalty.DoPunishment(player);

            // Assert
            Assert.That(player.Faculty, Is.EqualTo("FIOT"));
        }
    }
}
