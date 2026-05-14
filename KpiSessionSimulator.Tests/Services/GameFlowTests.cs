using KpiSessionSimulator.Models;
using Spectre.Console;
using Spectre.Console.Testing;

namespace KpiSessionSimulator.Tests.Services
{
    [TestFixture]
    public class GameFlowTests
    {
        private Question _testQuestion;

        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("SPECTRE_CONSOLE_INTERACTIVE", "true");

            _testQuestion = new Question
            {
                Text = "What is 2+2?",
                Options = new List<string> { "3", "4", "5" },
                IndexOfCorrectAnswer = 1
            };
        }

        [TearDown]
        public void Cleanup()
        {
            AnsiConsole.Console = AnsiConsole.Create(new AnsiConsoleSettings());
            Environment.SetEnvironmentVariable("SPECTRE_CONSOLE_INTERACTIVE", null);
        }

        [Test]
        public void FullCycle_CorrectAnswer_ResetsStreak()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            var player = new Player { WrongAnswersStreak = 2 };

            testConsole.Input.PushKey(ConsoleKey.DownArrow);
            testConsole.Input.PushKey(ConsoleKey.Enter);

            // Act
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(_testQuestion.Text)
                    .AddChoices(_testQuestion.Options));

            if (selected == _testQuestion.Options[_testQuestion.IndexOfCorrectAnswer])
            {
                player.WrongAnswersStreak = 0;
            }

            // Assert
            Assert.That(selected, Is.EqualTo("4"));
            Assert.That(player.WrongAnswersStreak, Is.EqualTo(0));
        }

        [Test]
        public void FullCycle_WrongAnswer_IncreasesStreak()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            var player = new Player { WrongAnswersStreak = 1 };

            testConsole.Input.PushKey(ConsoleKey.Enter);

            // Act
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(_testQuestion.Text)
                    .AddChoices(_testQuestion.Options));

            if (selected != _testQuestion.Options[_testQuestion.IndexOfCorrectAnswer])
            {
                player.WrongAnswersStreak++;
            }

            // Assert
            Assert.That(selected, Is.EqualTo("3"));
            Assert.That(player.WrongAnswersStreak, Is.EqualTo(2));
        }

        [Test]
        public void WrongAnswer_SelectMinigame()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            bool minigameStarted = false;

            testConsole.Input.PushKey(ConsoleKey.Enter);
            testConsole.Input.PushKey(ConsoleKey.DownArrow);
            testConsole.Input.PushKey(ConsoleKey.Enter);

            // Act
            var selectedAns = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(_testQuestion.Text)
                    .AddChoices(_testQuestion.Options));

            if (selectedAns != _testQuestion.Options[_testQuestion.IndexOfCorrectAnswer])
            {
                var choiceAfterFail = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Wrong answer! What will you choose?")
                        .AddChoices(new[] { "Accept Penalty", "Play Minigame" }));

                if (choiceAfterFail == "Play Minigame")
                {
                    minigameStarted = true;
                }
            }

            // Assert
            Assert.That(minigameStarted, Is.True);
        }
    }
}