using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Tests
{
    [TestFixture]
    public class AchievementTests
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _player = new Player
            {
                NickName = "Smn",
                Stats = new PlayerStats() 
            };
        }

        [Test]
        public void CheckAchievements_ShouldGiveFirstBloodAchievement()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 1;
            _player.Stats = stats;

            var teacher = new OpTeacher();

            // Act
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            // Assert
            Assert.That(_player.Stats.Achievements, Contains.Item("First Blood"));
        }

        [Test]
        public void CheckAchievements_ShouldGiveVeteranAchievement()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 10;
            _player.Stats = stats;

            var teacher = new OpTeacher();

            // Act
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            // Assert
            Assert.That(_player.Stats.Achievements, Contains.Item("Veteran"));
        }

        [Test]
        public void CheckAchievements_ShouldGiveLegendOfKpiAchievement()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 30;
            _player.Stats = stats;

            var teacher = new OpTeacher();

            // Act
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            // Assert
            Assert.That(_player.Stats.Achievements, Contains.Item("Legend of KPI"));
        }

        [Test]
        public void CheckAchievements_ShouldGiveMatanSurvivorAchievement()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 3; 
            stats.IsONSecondary = true; 
            _player.Stats = stats;

            var teacher = new MatanTeacher(); 

            // Act
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            // Assert
            Assert.That(_player.Stats.Achievements, Contains.Item("Matan Survivor"));
        }

        [Test]
        public void CheckAchievements_ShouldNotGiveMatanSurvivorAchievement()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 7;
            stats.IsONSecondary = false;
            _player.Stats = stats;

            var teacher = new MatanTeacher();

            // Act
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            // Assert
            Assert.That(_player.Stats.Achievements, Does.Not.Contain("Matan Survivor"));
        }

        [Test]
        public void CheckRouletteAchievements_ShouldGiveTerminatorAchievement()
        {
            // Arrange & Act 
            AchievementManager.CheckRouletteAchievements(_player);

            // Assert
            Assert.That(_player.Stats.Achievements, Contains.Item("Terminator"));
        }

        [Test]
        public void GiveAchievement_ShouldNotDuplicateAchievements()
        {
            // Arrange
            var stats = _player.Stats;
            stats.PassedExams = 1;
            _player.Stats = stats;

            var teacher = new OpTeacher();

            // Act 
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);
            AchievementManager.CheckAchievementsAfterExam(_player, teacher);

            int count = _player.Stats.Achievements.FindAll(a => a == "First Blood").Count;

            // Assert 
            Assert.That(count, Is.EqualTo(1));
        }
    }
}