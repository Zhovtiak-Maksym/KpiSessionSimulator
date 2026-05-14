using KpiSessionSimulator.Models;
using KpiSessionSimulator.Shop;

namespace KpiSessionSimulator.Tests.Shop
{
    [TestFixture]
    public class ImmunityTests
    {
        [Test]
        public void Execute_ShouldBuy_WhenEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 600, ImmunityCount = 2 };
            var perk = new Immunity();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(100));
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(3));
        }

        [Test]
        public void Execute_ShouldBuy_WhenEnoughTokens2()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 500, ImmunityCount = 5 };
            var perk = new Immunity();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(0));
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(6));
        }

        [Test]
        public void Execute_ShouldNotBuy_WhenNotEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 237, ImmunityCount = 1 };
            var perk = new Immunity();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(237));
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(1));
        }
    }
}
