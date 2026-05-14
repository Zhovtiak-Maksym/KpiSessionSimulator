using KpiSessionSimulator.Services;
using Spectre.Console;
using Spectre.Console.Testing;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class ProfileManagerTests
    {
        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("SPECTRE_CONSOLE_INTERACTIVE", "true");
        }

        [TearDown]
        public void Cleanup()
        {
            AnsiConsole.Console = AnsiConsole.Create(new AnsiConsoleSettings());
            Environment.SetEnvironmentVariable("SPECTRE_CONSOLE_INTERACTIVE", null);
        }

        [Test]
        public async Task LoginOrRegisterAsync_ShouldRegisterNewPlayer_UserSelectsRegister()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = testConsole;

            string nick = "Nick" + Guid.NewGuid().ToString().Substring(0, 5);

            testConsole.Input.PushKey(ConsoleKey.DownArrow);
            testConsole.Input.PushKey(ConsoleKey.Enter);

            testConsole.Input.PushTextWithEnter(nick);
            testConsole.Input.PushTextWithEnter("12345");

            // Act
            var newPlayer = await ProfileManager.LoginOrRegisterAsync();

            // Assert
            Assert.That(newPlayer, Is.Not.Null);
            Assert.That(newPlayer.NickName, Is.EqualTo(nick));
            Assert.That(newPlayer.Password, Is.EqualTo("12345"));
            Assert.That(newPlayer.Stats.Tokens, Is.EqualTo(0));
        }

        [Test]
        public async Task LoginOrRegisterAsync_ShouldSwitchToRegister_PlayerWasNotFound()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = testConsole;

            string nick = "Nick" + Guid.NewGuid().ToString().Substring(0, 5);

            testConsole.Input.PushKey(ConsoleKey.Enter);

            testConsole.Input.PushTextWithEnter(nick);
            testConsole.Input.PushTextWithEnter("12345");

            // Act
            var player = await ProfileManager.LoginOrRegisterAsync();

            // Assert
            Assert.That(player, Is.Not.Null);
            Assert.That(player.NickName, Is.EqualTo(nick));
            Assert.That(player.Password, Is.EqualTo("12345"));
        }

        [Test]
        public async Task LoginOrRegisterAsync_ShouldLoginSuccessfully_()
        {
            // Arrange
            var setupConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = setupConsole;

            string registerNick = "Nick" + Guid.NewGuid().ToString().Substring(0, 5);

            setupConsole.Input.PushKey(ConsoleKey.DownArrow);
            setupConsole.Input.PushKey(ConsoleKey.Enter);
            setupConsole.Input.PushTextWithEnter(registerNick);
            setupConsole.Input.PushTextWithEnter("12345");

            await ProfileManager.LoginOrRegisterAsync();

            // ACT
            var loginConsole = new TestConsole().Interactive(); 
            AnsiConsole.Console = loginConsole;

            loginConsole.Input.PushKey(ConsoleKey.Enter);
            loginConsole.Input.PushTextWithEnter(registerNick);
            loginConsole.Input.PushTextWithEnter("12345");

            var loggedPlayer = await ProfileManager.LoginOrRegisterAsync();

            // Assert
            Assert.That(loggedPlayer, Is.Not.Null);
            Assert.That(loggedPlayer.NickName, Is.EqualTo(registerNick));
            Assert.That(loggedPlayer.Password, Is.EqualTo("12345"));
        }
    }
}