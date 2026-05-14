using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;

namespace KpiSessionSimulator.Tests.Punishments
{
    [TestFixture]
    public class TokenPenaltyTests
    {
        [Test]
        public void DoPunishment_ShouldSubtract_Successfully()
        {
            // 1. Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 500 }; 
            var penalty = new TokenPenalty(200);             

            // 2. Act 
            penalty.DoPunishment(player);

            // 3. Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(300));
        }

        [Test]
        public void DoPunishment_ShouldNotGoBelowZero_WhenSubtractionIsHigherThenBalance()
        {
            // 1. Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 50 }; 
            var penalty = new TokenPenalty(300);          

            // 2. Act
            penalty.DoPunishment(player);

            // 3. Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(0));
        }
    }
}