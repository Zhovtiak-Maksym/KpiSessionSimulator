using KpiSessionSimulator.Factories;
using KpiSessionSimulator.Teachers;
using Spectre.Console;
using Spectre.Console.Testing;

namespace KpiSessionSimulator.Tests.Core
{
    [TestFixture]
    public class ExamFactoryTests
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
        public async Task GetExamSetUpAsync_ShouldReturnOpTeacherWithOPChoice()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            // Act
            var examData = await ExamFactory.GetExamSetUpAsync("OP (Skostariev)");

            // Assert
            Assert.That(examData, Is.Not.Null);
            Assert.That(examData.Teacher, Is.InstanceOf<OpTeacher>());
            Assert.That(examData.Questions, Is.Not.Null);
        }

        [Test]
        public async Task GetExamSetUpAsync_ShouldReturnAsdTeacherWithASDChoice()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            // Act
            var examData = await ExamFactory.GetExamSetUpAsync("ASD (Sulema)");

            // Assert
            Assert.That(examData, Is.Not.Null);
            Assert.That(examData.Teacher, Is.InstanceOf<ASDTeacher>());
            Assert.That(examData.Questions, Is.Not.Null);
        }

        [Test]
        public async Task GetExamSetUpAsync_ShouldReturnMatanTeacherWithMatanChoice()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            // Act
            var examData = await ExamFactory.GetExamSetUpAsync("Matan (Leheza)");

            // Assert
            Assert.That(examData, Is.Not.Null);
            Assert.That(examData.Teacher, Is.InstanceOf<MatanTeacher>());
            Assert.That(examData.Questions, Is.Not.Null);
        }

        [Test]
        public async Task GetExamSetUpAsync_ShouldReturnMatanTeacherWithInvalidChoice()
        {
            // Arrange
            var testConsole = new TestConsole().Interactive();
            AnsiConsole.Console = testConsole;

            // Act
            var examData = await ExamFactory.GetExamSetUpAsync("Smth");

            // Assert
            Assert.That(examData, Is.Not.Null);
            Assert.That(examData.Teacher, Is.InstanceOf<MatanTeacher>());
            Assert.That(examData.Questions, Is.Not.Null);
        }
    }
}