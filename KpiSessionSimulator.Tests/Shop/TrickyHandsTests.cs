using KpiSessionSimulator.Models;
using KpiSessionSimulator.Shop;

namespace KpiSessionSimulator.Tests.Shop
{
    [TestFixture]
    public class TrickyHandsTests
    {
        [Test]
        public void Execute_ShouldBuy_WhenEnoughTokens1()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 200, TrickyHandsCount = 2 };
            var perk = new TrickyHands();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(50));
            Assert.That(player.Stats.TrickyHandsCount, Is.EqualTo(3));
        }

        [Test]
        public void Execute_ShouldBuy_WhenEnoughTokens2()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 150, TrickyHandsCount = 0 };
            var perk = new TrickyHands();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(0));
            Assert.That(player.Stats.TrickyHandsCount, Is.EqualTo(1));
        }

        [Test]
        public void Execute_ShouldNotBuy_WhenNotEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 149, TrickyHandsCount = 2 };
            var perk = new TrickyHands();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(149));
            Assert.That(player.Stats.TrickyHandsCount, Is.EqualTo(2));
        }
    }
}