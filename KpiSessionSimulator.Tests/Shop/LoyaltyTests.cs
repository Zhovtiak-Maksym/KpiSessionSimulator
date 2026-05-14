using KpiSessionSimulator.Models;
using KpiSessionSimulator.Shop;

namespace KpiSessionSimulator.Tests.Shop
{
    [TestFixture]
    public class LoyaltyTests
    {
        [Test]
        public void Execute_ShouldBuy_WhenEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 500, LoyaltyCount = 0 };
            var perk = new Loyalty(); 

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(300));
            Assert.That(player.Stats.LoyaltyCount, Is.EqualTo(1));
        }

        [Test]
        public void Execute_ShouldNotBuy_WhenNotEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 67, LoyaltyCount = 0 };
            var perk = new Loyalty();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(67));
            Assert.That(player.Stats.LoyaltyCount, Is.EqualTo(0));
        }
    }
}
