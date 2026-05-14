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
            player.Stats = new PlayerStats { Tokens = 1000, ImmunityCount = 2 };
            var perk = new Immunity(); 

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(500));
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(3));
        }

        [Test]
        public void Execute_ShouldNotBuy_WhenNotEnoughTokens()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 499, ImmunityCount = 0 };
            var perk = new Immunity();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(499)); 
            Assert.That(player.Stats.ImmunityCount, Is.EqualTo(0));
        }
    }
}
