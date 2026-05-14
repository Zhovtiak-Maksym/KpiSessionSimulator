using KpiSessionSimulator.Minigames;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Tests.Minigames
{
    [TestFixture]
    public class BlackjackTests
    {
        [TestCase(21, 21, true)]  
        [TestCase(21, 15, true)]   
        [TestCase(20, 18, true)]   
        [TestCase(19, 17, true)]  
        [TestCase(18, 20, false)] 
        [TestCase(15, 17, false)]  
        [TestCase(19, 19, false)]  
        [TestCase(17, 17, false)]  
        public void SelectWinner_ShouldReturnCorrectResult_ForAllCombinations(int playerScore, int teacherScore, bool expectedResult)
        {
            // Arrange
            var game = new Blackjack();
            var player = new Player { NickName = "Smn" };
            var teacher = new OpTeacher();

            // Act
            bool result = game.SelectWinner(player, teacher, playerScore, teacherScore);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}