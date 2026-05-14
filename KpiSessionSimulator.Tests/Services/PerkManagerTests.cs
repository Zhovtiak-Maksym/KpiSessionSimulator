using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;
using Spectre.Console;
using Spectre.Console.Testing;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class PerkManagerTests
    {
        [TearDown]
        public void Cleanup()
        {
            AnsiConsole.Console = AnsiConsole.Create(new AnsiConsoleSettings());
        }

        [Test]
        public void UseEagleEye_ShouldRemoveWrongAnswer_UserSelectsYes()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = testConsole;
            testConsole.Input.PushKey(ConsoleKey.Enter);

            var player = new Player();
            player.Stats = new PlayerStats { EagleEyeCount = 1 };

            var question = new Question
            {
                Options = new List<string> { "Correct", "Wrong1", "Wrong2", "Wrong3" },
                IndexOfCorrectAnswer = 0
            };
            var manager = new PerkManager();

            // Act
            bool result = manager.UseEagleEye(player, question);
            bool hasRemoved = question.Options.Any(o => o.Contains("REMOVED"));

            // Assert
            Assert.That(result, Is.True);
            Assert.That(player.Stats.EagleEyeCount, Is.EqualTo(0));
            Assert.That(hasRemoved, Is.True);
        }

        [Test]
        public void UseEagleEye_ShouldNotUsePerk_WhenUserSelectsNo()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = testConsole;
            testConsole.Input.PushKey(ConsoleKey.DownArrow);
            testConsole.Input.PushKey(ConsoleKey.Enter);

            var player = new Player();
            player.Stats = new PlayerStats { EagleEyeCount = 1 };

            var question = new Question
            {
                Options = new List<string> { "Correct", "Wrong1", "Wrong2", "Wrong3" },
                IndexOfCorrectAnswer = 0
            };
            var manager = new PerkManager();

            // Act
            bool result = manager.UseEagleEye(player, question);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(player.Stats.EagleEyeCount, Is.EqualTo(1));
        }

        [Test]
        public void UseLoyalty_ReturnsNewQuestion_WhenUserSelectsYes()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = testConsole;
            testConsole.Input.PushKey(ConsoleKey.Enter);

            var player = new Player();
            player.Stats = new PlayerStats { LoyaltyCount = 1 };
            var manager = new PerkManager();
            var expectedAfterLoyaltyquestion = new Question { Text = "Smth" };

            // Act
            var result = manager.UseLoyalty(player, () => expectedAfterLoyaltyquestion);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("Smth"));
            Assert.That(player.Stats.LoyaltyCount, Is.EqualTo(0));
        }
    }
}