using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using NUnit.Framework;

namespace KpiSessionSimulator.Tests.Punishments
{
    [TestFixture]
    public class SecondaryPenaltyTests
    {
        [Test]
        public void DoPunishment_ShouldSetSecondary_WhenFirstFail()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { IsONSecondary = false, IsExpelled = false };
            var penalty = new SecondaryPenalty();

            // Act
            penalty.DoPunishment(player);

            // Assert
            Assert.That(player.Stats.IsONSecondary, Is.True);
            Assert.That(player.Stats.IsExpelled, Is.False);
        }

        [Test]
        public void DoPunishment_ShouldExpel_WhenOnSecondary()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { IsONSecondary = true, IsExpelled = false };
            var penalty = new SecondaryPenalty();

            // Act
            penalty.DoPunishment(player);

            // Assert
            Assert.That(player.Stats.IsExpelled, Is.True);
        }
    }
}