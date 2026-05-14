using KpiSessionSimulator.Minigames;

namespace KpiSessionSimulator.Tests.Minigames
{
    [TestFixture]
    public class RouletteTests
    {
        [Test]
        public void RouletteConstructor_ShouldSetCorrectStartUp()
        {
            // Arrange & Act
            var roulette = new Roulette();

            // Assert
            Assert.That(roulette.CurrBullets, Is.EqualTo(2));
            Assert.That(Roulette.BulletSlots, Is.EqualTo(6));
        }
    }
}
