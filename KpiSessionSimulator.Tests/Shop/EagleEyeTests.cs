using KpiSessionSimulator.Models;
using KpiSessionSimulator.Shop;

namespace KpiSessionSimulator.Tests.Shop
{
    [TestFixture]
    public class EagleEyeTests
    {
        [Test]
        public void Execute_ShouldSubtractTokensAndAddPerk_WhenEnoughMoney()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 1000, EagleEyeCount = 0 };
            var perk = new EagleEye();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(700));
            Assert.That(player.Stats.EagleEyeCount, Is.EqualTo(1));
        }

        [Test]
        public void Execute_NoChanges_WhenPlayerHasNoMoney()
        {
            // Arrange
            var player = new Player();
            player.Stats = new PlayerStats { Tokens = 5, EagleEyeCount = 0 };
            var perk = new EagleEye();

            // Act
            perk.Execute(player);

            // Assert
            Assert.That(player.Stats.Tokens, Is.EqualTo(5));
            Assert.That(player.Stats.EagleEyeCount, Is.EqualTo(0));
        }
    }
}